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
using API.UoW;
using LogClient;
using LogClient.Types;

namespace API.Controllers
{
    public class AppController : BaseApiController
    {
        LogClient.ILogger _logger;

        public AppController(IUnitOfWork uow, LogClient.ILogger logger) : base(uow)
        {
            _logger = logger;
        }

        [HttpGet("InitState")]
        public async Task<ActionResult<InitStateDto>> GetInitialStateAsync()
        {
            InitStateDto dto = new InitStateDto();

            dto.Technologies = await (from t in _uow.TechnologyRepo.All
                                      where t.IsActive
                                      select new TechnologyDto()
                                      {
                                          Name = t.Name,
                                          Amount = t.QuestionsAmount,
                                          Duration = t.DurationInMinutes
                                      }).ToArrayAsync();

            return dto;
        }

        [HttpGet("Technologies")]
        public async Task<ActionResult<TechnologyDto[]>> GetTechnologiesAsync()
        {
            return await (from t in _uow.TechnologyRepo.All
                          where t.IsActive
                          select new TechnologyDto()
                          {
                              Id = t.Id,
                              Name = t.Name,
                              Amount = t.QuestionsAmount,
                              Duration = t.DurationInMinutes
                          }).ToArrayAsync();
        }

        [HttpGet("logger")]
        public async Task<ActionResult<string>> GetLoggerAsync()
        {
            return await _logger.GenerateJavaScriptLoggerObjectAsync(Product.Tester);
        }
    }
}