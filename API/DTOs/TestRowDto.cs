
namespace API.DTOs
{
    public class TestRowDto
    {
        public TestRowDto(Test testDb)
        {
            TechName = testDb.Technology.Name;
            Score = testDb.FinalScore;
            Username = testDb.Username;
            TimeSpentInSeconds =
                testDb.FinishDate.HasValue ?
                    (int)(testDb.FinishDate - testDb.StartDate).Value.TotalSeconds : 0;
            Date = testDb.FinishDate.HasValue ?
                    testDb.FinishDate.Value.ToString("yyyy-MM-dd mm:HH") : "abandoned";
        }

        public string TechName { get; set; }

        public float? Score { get; set; }

        public string Username { get; set; }

        public int TimeSpentInSeconds { get; set; }

        public string Date { get; set; }
    }
}