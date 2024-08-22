using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database.Entities
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Answer1 { get; set; }

        [Required]
        public string Answer2 { get; set; }

        [Required]
        public string Answer3 { get; set; }

        [Required]
        public string Answer4 { get; set; }

        public byte CorrectAnswerNumber { get; set; }

        public bool IsActive { get; set; }

        public int TechnologyId { get; set; }

        public Technology Technology { get; set; }

        public ICollection<TestQuestion> TestQuestions{ get; set; }
    }
}