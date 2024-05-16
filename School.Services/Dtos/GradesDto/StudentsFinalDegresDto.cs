using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.GradesDto
{
    public class StudentsFinalDegresDto
    {
        public List<NameIdDto>Subjects { get; set; } = new List<NameIdDto>();
        public List<StudentFinalGrade> studentsFinalGrade { get; set; } = new List<StudentFinalGrade>();

    }
}
