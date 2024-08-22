using API.Database.Entities;

namespace API.Database
{
    public static class DbInitializer
    {
        // Add any initials here.
        public static void Initialize(TestDbContext ctx)
        {
            bool shouldWeSubmit = false;
            Technology[] techArray = new Technology[3];

            if (!ctx.Technologies.Any())
            {
                techArray[0] = new Technology() { Name = "Java Script", IsActive = true, DurationInMinutes = 10, QuestionsAmount = 10 };
                techArray[1] = new Technology() { Name = "C#", IsActive = true, DurationInMinutes = 12, QuestionsAmount = 8 };
                techArray[2] = new Technology() { Name = "C/C++", IsActive = true, DurationInMinutes = 6, QuestionsAmount = 12 };
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
                        Text = "Hundreds of the nation's top athletes are headed to Paris as part of Team USA in the 2024 Olympics.",
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
                        Text = "President Biden is meeting with Israeli Prime Minister Benjamin Netanyahu at the White House Thursday.",
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
                        Text = @"Vice President Harris has fought alongside Joe Biden to deliver historic accomplishments
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

            if (shouldWeSubmit)
            {
                ctx.SaveChanges();
            }
        }
    }
}