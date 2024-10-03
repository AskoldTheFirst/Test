using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Database;
using API.DTOs;
using API.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder.Extensions;
using API.Services;
using API.Database.Entities;
using System.Text.Json;
using API.UoW;

namespace API.Controllers
{
    public class StatisticsController : BaseApiController
    {
        private readonly AppCacheService _cache;

        public StatisticsController(AppCacheService cacheService, IUnitOfWork uow) : base(uow)
        {
            _cache = cacheService;
            _uow = uow;
        }

        [HttpGet("result-rows")]
        public async Task<ActionResult<PageDto<TestRowDto>>> GetResultRowsAsync([FromQuery] FilterDto filter)
        {
            var query = _uow.TestRepo.GetAll();
            query = ApplyFilterWhere(query, filter);

            int skipAmount = (filter.PageNumber - 1) * filter.PageSize;

            TestRowDto[] selectedRows = await (from q in query.Include(t => t.Technology)
                                               join t in _uow.TechnologyRepo.All
                                                   on q.TechnologyId equals t.Id
                                               orderby q.FinalScore descending
                                               select new TestRowDto(q))
                        .Skip(skipAmount)
                        .Take(filter.PageSize).ToArrayAsync();

            int totalAmount = await (from l in query select l).CountAsync();

            return new PageDto<TestRowDto>
            {
                Rows = selectedRows,
                Total = totalAmount
            };
        }

        [HttpGet("tops")]
        public async Task<ActionResult<TopDto[]>> GetTopsAsync(int topAmount)
        {
            List<TopDto> tops = [];

            var nameIdArray = await (from t in _uow.TechnologyRepo.All
                                     where t.IsActive
                                     select new { t.Name, t.Id }).ToArrayAsync();

            foreach (var t in nameIdArray)
            {
                TopLineDto[] lines = await (from tt in _uow.TestRepo.All
                                            where tt.TechnologyId == t.Id && tt.FinishDate != null && tt.FinalScore != null
                                            orderby tt.FinalScore descending
                                            select new TopLineDto
                                            {
                                                Login = tt.Username,
                                                Date = tt.FinishDate.Value.ToString("yyyy/MM/dd hh:mm"),
                                                Score = tt.FinalScore.Value
                                            }).Take(topAmount).ToArrayAsync();

                tops.Add(new TopDto
                {
                    Lines = lines,
                    TechName = t.Name
                });
            }

            return tops.ToArray();
        }

        [HttpGet("TopByTech")]
        public async Task<ActionResult<TopDto>> GetTopByTechnologyAsync(string technologyName, int topAmount)
        {
            TopLineDto[] lines = await (from tt in _uow.TestRepo.All
                                        join t in _uow.TechnologyRepo.All
                                           on tt.TechnologyId equals t.Id
                                        where t.Name == technologyName
                                        orderby tt.FinalScore descending
                                        select new TopLineDto
                                        {
                                            Login = tt.Username,
                                            Date = tt.FinishDate.Value.ToString("yyyy/MM/dd hh:mm"),
                                            Score = tt.FinalScore.Value
                                        }).Take(topAmount).ToArrayAsync();

            return new TopDto
            {
                Lines = lines,
                TechName = technologyName
            };
        }

        private IQueryable<Database.Entities.Test> ApplyFilterWhere(IQueryable<Database.Entities.Test> query, FilterDto param)
        {
            if (!String.IsNullOrEmpty(param.UserSearch))
            {
                query = query.Where(x => x.Username.Contains(param.UserSearch));
            }

            DateTime dtNow = DateTime.Now;
            switch (param.Period)
            {
                case TimePeriod.Last24Hours:
                    query = query.Where(x => EF.Functions.DateDiffDay(x.StartDate, dtNow) < 2);
                    break;

                case TimePeriod.Last7Days:
                    query = query.Where(x => EF.Functions.DateDiffDay(x.StartDate, dtNow) < 8);
                    break;

                case TimePeriod.Last30Days:
                    query = query.Where(x => EF.Functions.DateDiffDay(x.StartDate, dtNow) < 31);
                    break;
            }


            int[] ids = param.GetIds();
            bool isAllTechnologies = false;
            List<int> techIds = _cache.GetTechnologies().Select(x => x.Id).ToList();
            if (techIds.Count == ids.Length)
            {
                for (int i = 0; i < techIds.Count; ++i)
                {
                    for (int j = 0; j < ids.Length; ++j)
                    {
                        if (techIds[i] == ids[j])
                        {
                            techIds.RemoveAt(i);
                            break;
                        }
                    }
                }

                if (techIds.Count == 0)
                {
                    isAllTechnologies = true;
                }
            }

            if (!isAllTechnologies)
            {
                query = query.Where(x => ids.Contains(x.TechnologyId));
            }

            return query;
        }
    }
}