using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Entities;
using API.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{    
    public class QuestionsController : BaseApiController
    {
        
        private readonly TestDbContext _ctx;

        IUnitOfWork _uow;

        public QuestionsController(TestDbContext ctx, IUnitOfWork uow)
        {
            _ctx = ctx;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetQuestions()
        {
            return await _ctx.Questions.ToListAsync();
        }

        
        [HttpGet("question")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var product = await (from q in _uow.QuestionRepo.All where q.Id == id select q).SingleOrDefaultAsync();

            if (product == null) return NotFound();

            return product;
        }
    }
}