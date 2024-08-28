using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TechnologyDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Amount { get; set; }

        public int Duration { get; set; }
    }
}