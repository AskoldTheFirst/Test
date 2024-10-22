
namespace API.Database
{
    public static class DbInitializer
    {
        // Add any initials here.
        public static void Initialize(TestDbContext ctx)
        {
            bool shouldWeSubmit = false;
            Technology[] techArray = new Technology[5];

            if (!ctx.Technologies.Any())
            {
                techArray[0] = new Technology() { Name = "Java Script", IsActive = true, DurationInMinutes = 8, QuestionsAmount = 10 };
                techArray[1] = new Technology() { Name = "C#", IsActive = true, DurationInMinutes = 4, QuestionsAmount = 8 };
                techArray[2] = new Technology() { Name = "C/C++", IsActive = true, DurationInMinutes = 10, QuestionsAmount = 12 };
                techArray[3] = new Technology() { Name = "MSSQL", IsActive = true, DurationInMinutes = 10, QuestionsAmount = 10 };
                techArray[4] = new Technology() { Name = "English", IsActive = true, DurationInMinutes = 12, QuestionsAmount = 20 };
                
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
                    new() {
                        Text = "Which of the following is not a JavaScript data type?",
                        Answer1 = "bigint",
                        Answer2 = "number",
                        Answer3 = "float",
                        Answer4 = "symbol",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new() {
                        Text = "Which of the following statements is not true?",
                        Answer1 = "A variable can be used before it has been declared.",
                        Answer2 = "Variables defined with let and const are hoisted to the top of the block without initialization and can be used in run-time there.",
                        Answer3 = "Hoisting in JavaScript is the default behavior of moving all declarations to the top of the current scope.",
                        Answer4 = "JavaScript only hoists declarations, not initializations.",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "What is the most correct way to clone an object in JavaScript deeply?",
                        Answer1 = "let clone = {... obj};",
                        Answer2 = "let clone = Object.assign({}, obj);",
                        Answer3 = "let clone = JSON.parse(JSON.stringify(obj));",
                        Answer4 = "let clone = structuredClone(obj);",
                        CorrectAnswerNumber = 4,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "Sealing an object this way: \"Object.seal(obj);\" will allow:",
                        Answer1 = "nothing",
                        Answer2 = "adding new properties",
                        Answer3 = "changing of existing properties",
                        Answer4 = "removing properties from that object",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "When will \'this\' be empty for such a method?: function foo() {console.log(this);}",
                        Answer1 = "foo();",
                        Answer2 = "new foo();",
                        Answer3 = "foo.call(0);",
                        Answer4 = "foo.apply(\'just a string\');",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "Which of the following comparisons will retrieve the truth?",
                        Answer1 = "(2 == \'2\' && 2 === \'2\')",
                        Answer2 = "(null == undefined && 0 == -0)",
                        Answer3 = "(NaN == NaN && false == 0)",
                        Answer4 = "([1, 2] === \'1,2\' || NaN === NaN)",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "Which is not a Promise state?",
                        Answer1 = "Suspended",
                        Answer2 = "Fulfilled",
                        Answer3 = "Settled",
                        Answer4 = "Pending",
                        CorrectAnswerNumber = 1,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "What would be the result of: 4 + 1 + \'2\'?",
                        Answer1 = "NaN",
                        Answer2 = "7",
                        Answer3 = "\'52\'",
                        Answer4 = "52",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "Which company developed JavaScript?",
                        Answer1 = "IBM",
                        Answer2 = "Intel",
                        Answer3 = "Netscape",
                        Answer4 = "Mosaic",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "What kind of loop is not supported in JavaScript?",
                        Answer1 = "for/of",
                        Answer2 = "do/while",
                        Answer3 = "for/each",
                        Answer4 = "for/in",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "What will be the result of the following: (5 > 4 > 3)",
                        Answer1 = "a run-time error",
                        Answer2 = "true",
                        Answer3 = "false",
                        Answer4 = "NaN",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[0].Id,
                        Technology = techArray[0]
                    },

                    new Question
                    {
                        Text = "Which type/way allows you to set the stack size for a new thread you are creating?",
                        Answer1 = "Task",
                        Answer2 = "ThreadPool",
                        Answer3 = "Delegate.BeginInvoke",
                        Answer4 = "Thread",
                        CorrectAnswerNumber = 4,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "Which of the following is used for performing an asynchronous method execution?",
                        Answer1 = "class Thread",
                        Answer2 = "IAsyncResult",
                        Answer3 = "Delegate",
                        Answer4 = "class ThreadPool",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "What is the result of this expression? (4 << 2 + 1)",
                        Answer1 = "InvalidOperationException",
                        Answer2 = "2",
                        Answer3 = "17",
                        Answer4 = "32",
                        CorrectAnswerNumber = 4,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "What is the result of this expression? (15 ^ 3 | 1)",
                        Answer1 = "15",
                        Answer2 = "14",
                        Answer3 = "13",
                        Answer4 = "12",
                        CorrectAnswerNumber = 3,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "How to change the stack size in .Net applications?",
                        Answer1 = "that's impossible",
                        Answer2 = "by editing PE manually",
                        Answer3 = "applications retrieve its stack size from the current OS",
                        Answer4 = "using the editbin.exe",
                        CorrectAnswerNumber = 4,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "In which cases should we use the 'var' keyword for declaring variables in C#?",
                        Answer1 = "whenever it is possible",
                        Answer2 = "when the exact variable type is obvious",
                        Answer3 = "when the specific type of the variable is tedious to type on the keyboard",
                        Answer4 = "only when we have no other option",
                        CorrectAnswerNumber = 4,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "What is true regarding calling this method: Task::ConfigureAwait(false);",
                        Answer1 = "It runs the rest of the code on the same thread it was run before.",
                        Answer2 = "It is recommended everywhere where coming back to the same SynchronizationContext is not needed.",
                        Answer3 = "It runs the rest of the code on the same SynchronizationContext it was run before.",
                        Answer4 = "It can lead to a run-time deadlock.",
                        CorrectAnswerNumber = 2,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "What is true regarding the \'volitile\' keyword in C#?",
                        Answer1 = "It prevents any kind of optimization or caching with such variables.",
                        Answer2 = "It creates a critical section around reading/writing of such variables.",
                        Answer3 = "It increases the performance of using such variables in multithreading environments.",
                        Answer4 = "The \'volitile\' keyword is applicable to any primitive types like \'float\', \'double\' or \'long\'.",
                        CorrectAnswerNumber = 1,
                        TechnologyId = techArray[1].Id,
                        Technology = techArray[1]
                    },

                    new Question
                    {
                        Text = "float or double",
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
                        Text = "Which way we can lock asynchroniously",
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

            if (!ctx.Tests.Any())
            {
                List<Test> tests = new List<Test>();

                tests.Add(new Test{
                    FinalScore = 88.5f,
                    FinishDate = DateTime.Now.AddHours(-234),
                    StartDate = DateTime.Now.AddHours(-234),
                    Technology = techArray[0],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 34.2f,
                    FinishDate = DateTime.Now.AddHours(-101),
                    StartDate = DateTime.Now.AddHours(-101),
                    Technology = techArray[0],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 49.0f,
                    FinishDate = DateTime.Now.AddHours(-76),
                    StartDate = DateTime.Now.AddHours(-76),
                    Technology = techArray[0],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 99.45f,
                    FinishDate = DateTime.Now.AddHours(-34),
                    StartDate = DateTime.Now.AddHours(-34),
                    Technology = techArray[0],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 65.5f,
                    FinishDate = DateTime.Now.AddHours(-2004),
                    StartDate = DateTime.Now.AddHours(-2004),
                    Technology = techArray[1],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 88.5f,
                    FinishDate = DateTime.Now.AddHours(-349),
                    StartDate = DateTime.Now.AddHours(-349),
                    Technology = techArray[1],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 17.0f,
                    FinishDate = DateTime.Now.AddHours(-16),
                    StartDate = DateTime.Now.AddHours(-16),
                    Technology = techArray[2],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 88.5f,
                    FinishDate = DateTime.Now.AddHours(-700),
                    StartDate = DateTime.Now.AddHours(-700),
                    Technology = techArray[2],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 91.4f,
                    FinishDate = DateTime.Now.AddHours(-435),
                    StartDate = DateTime.Now.AddHours(-435),
                    Technology = techArray[2],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 76.5f,
                    FinishDate = DateTime.Now.AddHours(-555),
                    StartDate = DateTime.Now.AddHours(-555),
                    Technology = techArray[2],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 4.9f,
                    FinishDate = DateTime.Now.AddHours(-198),
                    StartDate = DateTime.Now.AddHours(-198),
                    Technology = techArray[2],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 0.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 100.0f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 2.3f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
                });

                tests.Add(new Test{
                    FinalScore = 6.6f,
                    FinishDate = DateTime.Now.AddHours(-56),
                    StartDate = DateTime.Now.AddHours(-56),
                    Technology = techArray[4],
                    Username = "testUser"
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