using API.Database.Entities;

namespace API.Database
{
    public static class DbInitializer
    {
        // Add any initials here.
        public static void Initialize(TestDbContext ctx)
        {
            bool shouldWeSubmit = false;
            Technology[] techArray = new Technology[5];
            User testUser1 = null;

            if (!ctx.Technologies.Any())
            {
                techArray[0] = new Technology() { Name = "Java Script", IsActive = true, SecondsForOneAnswer = 30, QuestionsAmount = 10 };
                techArray[1] = new Technology() { Name = "C#", IsActive = true, SecondsForOneAnswer = 40, QuestionsAmount = 8 };
                techArray[2] = new Technology() { Name = "C/C++", IsActive = true, SecondsForOneAnswer = 20, QuestionsAmount = 12 };
                techArray[3] = new Technology() { Name = "MSSQL", IsActive = true, SecondsForOneAnswer = 45, QuestionsAmount = 10 };
                techArray[4] = new Technology() { Name = "English", IsActive = true, SecondsForOneAnswer = 60, QuestionsAmount = 20 };
                
                ctx.Technologies.AddRange(techArray);
                shouldWeSubmit = true;
            }
            else
            {
                techArray = ctx.Technologies.Take(3).ToArray();
            }

            if (!ctx.Questions.Any())
            {
                var questions = new List<Question>()
                {
                    new Question
                    {
                        Text = "1 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "2 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "3 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "4 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "5 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "6 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "7 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "8 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "9 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "10 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "11 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "12 JS Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
                        Answer1 = "aaa",
                        Answer2 = "bbb",
                        Answer3 = "ccc",
                        Answer4 = "ddd",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "1 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "2 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "3 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "4 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "5 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "6 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "7 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "8 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "9 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "10 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "11 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "12 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "13 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "14 C#President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
                        Answer1 = "qqq",
                        Answer2 = "rrf",
                        Answer3 = "ttt",
                        Answer4 = "jjj",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = @"1 C/C++Vice President Harris has fought alongside Joe Biden to deliver historic accomplishments
                                    and create a better life for A 10-year-old boy died Wednesday in a drone attack
                                    targeting soldiers in Colombia, the first death of its kind in the country that has struggled to rein in guerrilla violence.",
                        Answer1 = "ccc",
                        Answer2 = "uuu",
                        Answer3 = "mmm",
                        Answer4 = "zzz",
                        CorrectAnswerNumber = 4,
                        TechnologyId = techArray[2].Id,
                        Technology = techArray[2]
                    }
                };

                ctx.Questions.AddRange(questions);
                shouldWeSubmit = true;
            }

            if (!ctx.Users.Any())
            {
                testUser1 = new User
                {
                    IsActive = true,
                    Email = "test@test.com",
                    Passsword = "11",
                    Login = "TestUser",
                    LastLogin = DateTime.Now,
                    Registered = DateTime.Now.AddDays(-20)
                };

                ctx.Users.Add(testUser1);
                shouldWeSubmit = true;
            }
            else
            {
                testUser1 = ctx.Users.Take(1).Single();
            }

            if (!ctx.Tests.Any())
            {
                List<Test> tests = new List<Test>();

                tests.Add(new Test{
                    FinalScore = 88.5f,
                    FinishDate = DateTime.Now.AddHours(-234),
                    StartDate = DateTime.Now.AddHours(-234),
                    Technology = techArray[0],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 34.2f,
                    FinishDate = DateTime.Now.AddHours(-101),
                    StartDate = DateTime.Now.AddHours(-101),
                    Technology = techArray[0],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 49.0f,
                    FinishDate = DateTime.Now.AddHours(-76),
                    StartDate = DateTime.Now.AddHours(-76),
                    Technology = techArray[0],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 99.45f,
                    FinishDate = DateTime.Now.AddHours(-34),
                    StartDate = DateTime.Now.AddHours(-34),
                    Technology = techArray[0],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 65.5f,
                    FinishDate = DateTime.Now.AddHours(-2004),
                    StartDate = DateTime.Now.AddHours(-2004),
                    Technology = techArray[1],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 88.5f,
                    FinishDate = DateTime.Now.AddHours(-349),
                    StartDate = DateTime.Now.AddHours(-349),
                    Technology = techArray[1],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 17.0f,
                    FinishDate = DateTime.Now.AddHours(-16),
                    StartDate = DateTime.Now.AddHours(-16),
                    Technology = techArray[2],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 88.5f,
                    FinishDate = DateTime.Now.AddHours(-700),
                    StartDate = DateTime.Now.AddHours(-700),
                    Technology = techArray[2],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 91.4f,
                    FinishDate = DateTime.Now.AddHours(-435),
                    StartDate = DateTime.Now.AddHours(-435),
                    Technology = techArray[2],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 76.5f,
                    FinishDate = DateTime.Now.AddHours(-555),
                    StartDate = DateTime.Now.AddHours(-555),
                    Technology = techArray[2],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 4.9f,
                    FinishDate = DateTime.Now.AddHours(-198),
                    StartDate = DateTime.Now.AddHours(-198),
                    Technology = techArray[2],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 100.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 2.3f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                tests.Add(new Test{
                    FinalScore = 6.6f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    User = testUser1,
                    UserId = testUser1.Id
                });

                ctx.Tests.AddRange(tests);
                shouldWeSubmit = true;
            }

            if (shouldWeSubmit)
            {
                ctx.SaveChanges();
            }
        }
    }
}