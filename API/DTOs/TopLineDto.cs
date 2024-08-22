using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TopLineDto
    {
        public string Login { get; set; }

        public float Score { get; set; }

        public DateTime Date { get; set; }
    }
}