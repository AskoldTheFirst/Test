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
using Microsoft.AspNetCore.Identity;
using LogClient;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly LogClient.ILogger _logger;

        private readonly ITracer _tracer;

        private readonly TestDbContext _ctx;

        private readonly AppCacheService _cache;

        private readonly UserManager<User> _userManager;


        public TestController(
            TestDbContext context,
            LogClient.ILogger logger,
            AppCacheService cacheService,
            UserManager<User> userManager,
            ITracer tracer)
        {
            _ctx = context;
            _logger = logger;
            _cache = cacheService;
            _userManager = userManager;
            _tracer = tracer;
        }

        [Authorize]
        [HttpPost("initiate-new-test")]
        public async Task<ActionResult<InitTestResultDto>> InitiateNewTest(string techName)
        {
            string user = User.Identity.Name;

            long sessionId = DateTime.Now.Ticks;
            await _tracer.TraceAsync("InitiateNewTest", user, sessionId, sessionId);

            int[] randomQuestionIds = GenerateRandomQuestionsForTest(
                techName, out int questionAmount, out int technologyId);

            Test newTest = new()
            {
                TechnologyId = technologyId,
                StartDate = DateTime.Now,
                Username = user
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

            Technology currentTechnology = _cache.GetTechnologyByName(techName);
            InitTestResultDto result = new();
            result.TestId = newTest.Id;
            result.TotalAmount = currentTechnology.QuestionsAmount;
            result.SecondsLeft = currentTechnology.DurationInMinutes * 60;
            result.TechnologyName = currentTechnology.Name;

            await _tracer.TraceAsync("InitiateNewTest", user, DateTime.Now.Ticks, sessionId);

            return result;
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
            string userName = User.Identity.Name;

            long sessionId = DateTime.Now.Ticks;
            await _tracer.TraceAsync("NextQuestion", userName, sessionId, sessionId);

            QuestionDto nextQuestion = await (from tq in _ctx.TestQuestions
                                              join q in _ctx.Questions on tq.QuestionId equals q.Id
                                              join t in _ctx.Tests on tq.TestId equals t.Id
                                              where tq.AnswerDate == null && tq.TestId == testId && t.Username == userName
                                              orderby tq.RequestDate descending
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

            if (nextQuestion != null)
            {
                TestQuestion nextTestQuestion = await (from tq in _ctx.TestQuestions
                                                       where tq.TestId == testId && tq.QuestionId == nextQuestion.QuestionId
                                                       select tq).SingleAsync();

                nextTestQuestion.RequestDate = DateTime.Now;

                await _ctx.SaveChangesAsync();
            }

            await _tracer.TraceAsync("NextQuestion", userName, DateTime.Now.Ticks, sessionId);

            return nextQuestion;
        }

        [Authorize]
        [HttpGet("next-question-state")]
        public async Task<ActionResult<NextQuestionStateDto>> NextQuestionState(int? testId)
        {
            string userName = User.Identity.Name;
            Test currentTest = null;

            if (testId.HasValue)
            {
                currentTest = await (from t in _ctx.Tests
                                     where t.Username == userName && t.Id == testId && t.FinishDate == null
                                     orderby t.StartDate descending
                                     select t).SingleOrDefaultAsync();
            }
            else
            {
                currentTest = await (from t in _ctx.Tests
                                     where t.Username == userName && t.FinishDate == null && (DateTime.Now - t.StartDate).TotalMinutes < 90
                                     orderby t.StartDate descending
                                     select t).FirstOrDefaultAsync();
            }

            int currentTestId = currentTest.Id;

            QuestionDto nextQuestion = await (from tq in _ctx.TestQuestions
                                              join q in _ctx.Questions on tq.QuestionId equals q.Id
                                              where tq.AnswerDate == null && tq.TestId == currentTestId
                                              orderby tq.RequestDate descending
                                              select new QuestionDto
                                              {
                                                  Text = q.Text,
                                                  Answer1 = q.Answer1,
                                                  Answer2 = q.Answer2,
                                                  Answer3 = q.Answer3,
                                                  Answer4 = q.Answer4,
                                                  QuestionId = q.Id,
                                                  TestId = currentTestId,
                                                  TestQuestionId = tq.Id
                                              }).FirstOrDefaultAsync();

            if (nextQuestion != null)
            {
                TestQuestion nextTestQuestion = await (from tq in _ctx.TestQuestions
                                                       where tq.TestId == currentTestId && tq.QuestionId == nextQuestion.QuestionId
                                                       select tq).SingleAsync();

                nextTestQuestion.RequestDate = DateTime.Now;

                await _ctx.SaveChangesAsync();
            }

            Technology testTechnology = _cache.GetTechnologyById(currentTest.TechnologyId);
            int secondsLeft = testTechnology.DurationInMinutes * 60 - (int)(DateTime.Now - currentTest.StartDate).TotalSeconds;
            NextQuestionStateDto nextState = new();
            nextState.Question = nextQuestion;
            nextState.QuestionNumber = await GetAmountOfAlreadyAnsweredQuestionsAsync(currentTestId) + 1;
            nextState.TotalAmount = testTechnology.QuestionsAmount;
            nextState.SecondsLeft = secondsLeft;
            nextState.TechnologyName = testTechnology.Name;

            return nextState;
        }

        [Authorize]
        [HttpGet("test-result")]
        public async Task<ActionResult<TestResultDto>> TestResult(int testId)
        {
            string userName = User.Identity.Name;

            int amount = await GetAmountOfAlreadyAnsweredQuestionsAsync(testId);

            return await (from t in _ctx.Tests
                          where t.Id == testId && t.Username == userName
                          select new TestResultDto
                          {
                              Score = (float)t.FinalScore,
                              TimeSpentInSeconds = (int)(t.FinishDate.Value - t.StartDate).TotalSeconds,
                              QuestionsAmount = amount
                          }).SingleAsync();
        }

        [Authorize]
        [HttpPut("complete-test")]
        public async Task<ActionResult<TestResultDto>> CompleteTestAndRetrieveResult(int testId)
        {
            string userName = User.Identity.Name;

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
                                      where t.Id == testId && t.Username == userName
                                      select t).SingleAsync();

            currentTest.FinalScore = finalScore;
            currentTest.FinishDate = DateTime.Now;

            await _ctx.SaveChangesAsync();

            return await TestResult(testId);
        }

        private int[] GenerateRandomQuestionsForTest(string techName, out int questionAmount, out int testTechnologyId)
        {
            Technology[] allTechnologies = _cache.GetTechnologies();
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

        private async Task<int> GetAmountOfAlreadyAnsweredQuestionsAsync(int testId)
        {
            return await (from q in _ctx.TestQuestions
                          where q.TestId == testId && q.AnswerDate != null
                          select q.Id).CountAsync();
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