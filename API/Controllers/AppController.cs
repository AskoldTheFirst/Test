using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Database;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AppController : BaseApiController
    {
        private readonly ILogger<AppController> _logger;
        private readonly TestDbContext _ctx;

        public AppController(TestDbContext context, ILogger<AppController> logger)
        {
            _ctx = context;
            _logger = logger;
        }

        [HttpGet("InitState")]
        public async Task<ActionResult<InitStateDto>> GetInitialStateAsync()
        {
            InitStateDto dto = new InitStateDto();

            dto.Technologies = await (from t in _ctx.Technologies
                                      where t.IsActive
                                      select new TechnologyDto()
                                      {
                                          Name = t.Name,
                                          Amount = t.QuestionsAmount,
                                          Duration = t.SecondsForOneAnswer
                                      }).ToArrayAsync();

            return dto;
        }

        [HttpGet("Technologies")]
        public async Task<ActionResult<TechnologyDto[]>> GetTechnologiesAsync()
        {
            return await (from t in _ctx.Technologies
                            where t.IsActive
                            select new TechnologyDto() {
                                Id = t.Id,
                                Name = t.Name,
                                Amount = t.QuestionsAmount,
                                Duration = t.SecondsForOneAnswer }).ToArrayAsync();
        }
    }
}