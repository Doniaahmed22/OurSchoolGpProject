using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.GradesDto
{
    public class SubjectFinalDegree
    {
        public NameIdDto Subject { get; set; } = new NameIdDto();
        public int FinalDegree { get; set; } 
    }
}
