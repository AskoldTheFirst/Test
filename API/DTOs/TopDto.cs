using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TopDto
    {
        public string TechName { get; set; }

        public TopLineDto[] Lines { get; set; }
    }
}