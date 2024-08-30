using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class QuestionDto
    {
        public int TestId { get; set; }

        public int QuestionId { get; set; }

        public int TestQuestionId { get; set; }

        public string Text { get; set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }

        public string Answer3 { get; set; }

        public string Answer4 { get; set; }
    }
}