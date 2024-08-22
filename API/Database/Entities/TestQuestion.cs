
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace API.Database.Entities
{
    public class TestQuestion
    {
        public int Id { get; set; }

        public byte? AnswerNumber { get; set; }

        public byte? AnswerPoints { get; set; }

        public DateTime? AnswerDate { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}