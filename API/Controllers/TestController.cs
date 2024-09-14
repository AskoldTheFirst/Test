using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using API.Database.Entities;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using API.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly ILogger<AppController> _logger;
        private readonly TestDbContext _ctx;
        private readonly AppCacheService _cache;

        public TestController(TestDbContext context, ILogger<AppController> logger, AppCacheService cacheService)
        {
            _ctx = context;
            _logger = logger;
            _cache = cacheService;
        }

        [Authorize]
        [HttpPost("initiate-new-test")]
        public async Task<ActionResult<int>> InitiateNewTest(string techName)
        {
            Console.WriteLine("API-TechName: " + techName);
            Console.WriteLine("API-TechName: " + HttpUtility.HtmlEncode(techName));
            int[] randomQuestionIds = GenerateRandomQuestionsForTest(
                techName, out int questionAmount, out int technologyId);

            IDbContextTransaction transaction =
                await _ctx.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                Test newTest = new()
                {
                    TechnologyId = technologyId,
                    StartDate = DateTime.Now,
                    Username = "testUser"
                };

                _ctx.Tests.Add(newTest);
                await _ctx.SaveChangesAsync();

                TestQuestion[] generatedQuestions = new TestQuestion[questionAmount];

                for (int i = 0; i < questionAmount; ++i)
                {
                    generatedQuestions[i] = new TestQuestion()
                    {
                        QuestionId = randomQuestionIds[i],
                        TestId = newTest.Id
                    };
                }

                _ctx.TestQuestions.AddRange(generatedQuestions);
                await _ctx.SaveChangesAsync();

                await transaction.CommitAsync();

                return newTest.Id;
             }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost("answer")]
        public async Task<ActionResult> Answer(int testId, int questionId, byte answerNumber)
        {
            var answerPointAndTechId = await (from q in _ctx.Questions
                                              where q.Id == questionId
                                              select new { q.CorrectAnswerNumber, q.TechnologyId }).SingleAsync();

            var currentTestQuestion = await (from tq in _ctx.TestQuestions
                                             where tq.TestId == testId && tq.QuestionId == questionId
                                             select tq).SingleAsync();

            currentTestQuestion.AnswerNumber = answerNumber;
            currentTestQuestion.AnswerPoints = (byte)(answerNumber == answerPointAndTechId.CorrectAnswerNumber ? 1 : 0);
            currentTestQuestion.AnswerDate = DateTime.Now;

            await _ctx.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpGet("next-question")]
        public async Task<ActionResult<QuestionDto>> NextQuestion(int testId)
        {
            IDbContextTransaction transaction =
                await _ctx.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            QuestionDto nextQuestion = await (from tq in _ctx.TestQuestions
                                              join q in _ctx.Questions on tq.QuestionId equals q.Id
                                              where tq.RequestDate == null && tq.TestId == testId
                                              select new QuestionDto
                                              {
                                                  Text = q.Text,
                                                  Answer1 = q.Answer1,
                                                  Answer2 = q.Answer2,
                                                  Answer3 = q.Answer3,
                                                  Answer4 = q.Answer4,
                                                  QuestionId = q.Id,
                                                  TestId = testId,
                                                  TestQuestionId = tq.Id
                                              }).FirstOrDefaultAsync();

            // This should happen when we have answerd all test questions only.
            if (nextQuestion != null)
            {
                TestQuestion nextTestQuestion = await (from tq in _ctx.TestQuestions
                                                       where tq.TestId == testId && tq.QuestionId == nextQuestion.QuestionId
                                                       select tq).SingleAsync();

                nextTestQuestion.RequestDate = DateTime.Now;

                await _ctx.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return nextQuestion;
        }

        [Authorize]
        [HttpGet("test-result")]
        public async Task<ActionResult<TestResultDto>> TestResult(int testId)
        {
            return await (from t in _ctx.Tests
                          where t.Id == testId
                          select new TestResultDto
                          {
                              Score = (float)t.FinalScore,
                              TimeSpentInSeconds = (int)(t.FinishDate.Value - t.StartDate).TotalSeconds
                          }).SingleAsync();
        }

        [Authorize]
        [HttpPut("complete-test")]
        public async Task<ActionResult<TestResultDto>> CompleteTestAndRetrieveResult(int testId)
        {
            var totalPointsAndAmout = await (from tq in _ctx.TestQuestions
                                             where tq.TestId == testId
                                             group tq by 1 into grp
                                             select new
                                             {
                                                 Amount = grp.Count(),
                                                 Total = grp.Sum(t => (int)t.AnswerPoints)
                                             }).SingleAsync();

            float finalScore = totalPointsAndAmout.Total / (float)totalPointsAndAmout.Amount * 100;

            Test currentTest = await (from t in _ctx.Tests
                                      where t.Id == testId
                                      select t).SingleAsync();

            currentTest.FinalScore = finalScore;
            currentTest.FinishDate = DateTime.Now;

            await _ctx.SaveChangesAsync();

            return await TestResult(testId);
        }

        private int[] GenerateRandomQuestionsForTest(string techName, out int questionAmount, out int testTechnologyId)
        {
            Technology[] allTechnologies = _cache.GetEntiryTechnology();
            var amountAndId = (from t in allTechnologies
                               where string.Compare(t.Name, techName, true) == 0
                               select new { t.QuestionsAmount, t.Id }).Single();

            questionAmount = amountAndId.QuestionsAmount;
            testTechnologyId = amountAndId.Id;

            int[] ids = _cache.GetQuestionIds(techName);

            List<int> randomQuestionIds = new List<int>(questionAmount);
            foreach (int index in GenerateRandomUniqueNumbersFromZero(ids.Length, questionAmount))
                randomQuestionIds.Add(ids[index]);

            return [.. randomQuestionIds];
        }

        private static int[] GenerateRandomUniqueNumbersFromZero(int to, int amountOfNumbers)
        {
            if (amountOfNumbers > to)
            {
                throw new ArgumentException("");
            }

            List<int> newRandomIds = new List<int>();
            Random rand = new Random(DateTime.Now.Millisecond);
            while (amountOfNumbers > 0)
            {
                int newId = rand.Next(to);
                if (newRandomIds.Contains(newId))
                {
                    continue;
                }

                newRandomIds.Add(newId);
                --amountOfNumbers;
            }

            return [.. newRandomIds];
        }
    }
}