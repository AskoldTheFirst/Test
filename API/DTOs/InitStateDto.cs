using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class InitStateDto
    {
        public TechnologyDto[] Technologies { get; set; }

        public TopDto[] Tops { get; set; }
    }
}