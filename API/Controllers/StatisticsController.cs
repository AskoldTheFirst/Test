using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Database;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder.Extensions;

namespace API.Controllers
{
    public class StatisticsController : BaseApiController
    {
        private readonly ILogger<AppController> _logger;
        private readonly TestDbContext _ctx;

        public StatisticsController(TestDbContext context, ILogger<AppController> logger)
        {
            _ctx = context;
            _logger = logger;
        }

        [HttpGet("tops")]
        public async Task<ActionResult<TopDto[]>> GetTopsAsync(int topAmount)
        {
            List<TopDto> tops = new List<TopDto>();

            var nameIdArray = await (from t in _ctx.Technologies
                                     where t.IsActive
                                     select new { t.Name, t.Id }).ToArrayAsync();

            foreach (var t in nameIdArray)
            {
                TopLineDto[] lines = await (from tt in _ctx.Tests
                                            join u in _ctx.Users
                                                on tt.UserId equals u.Id
                                            where tt.TechnologyId == t.Id && tt.FinishDate != null && tt.FinalScore != null
                                            orderby tt.FinalScore descending
                                            select new TopLineDto
                                            {
                                                Login = u.Login,
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
            TopLineDto[] lines = await (from tt in _ctx.Tests
                                        join u in _ctx.Users
                                            on tt.UserId equals u.Id
                                        join t in _ctx.Technologies
                                           on tt.TechnologyId equals t.Id
                                        where t.Name == technologyName
                                        orderby tt.FinalScore descending
                                        select new TopLineDto
                                        {
                                            Login = u.Login,
                                            Date = tt.FinishDate.Value.ToString("yyyy/MM/dd hh:mm"),
                                            Score = tt.FinalScore.Value
                                        }).Take(topAmount).ToArrayAsync();

            return new TopDto
            {
                Lines = lines,
                TechName = technologyName
            };
        }
    }
}