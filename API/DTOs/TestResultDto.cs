using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TestResultDto
    {
        public float Score { get; set; }

        public int TimeSpentInSeconds { get; set; }

        public int QuestionsAmount { get; set; }
    }
}