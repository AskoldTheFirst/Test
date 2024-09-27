using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class Test
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public float? FinalScore { get; set; }

        public int TechnologyId { get; set; }

        public Technology Technology { get; set; }

        [DefaultValue("getdate()")]
        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public ICollection<TestQuestion> TestQuestions{ get; set; }

        // Just for curiosity ...
        public string IpAddress { get; set; }
    }
}