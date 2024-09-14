using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{    
    public class QuestionsController : BaseApiController
    {
        
        private readonly TestDbContext _ctx;

        public QuestionsController(TestDbContext ctx)
        {
            _ctx = ctx;
            
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetQuestions()
        {
            return await _ctx.Questions.ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var product = await _ctx.Questions.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }
    }
}