using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.GradesDto
{
    public class StudentGradeDto
    {
        public NameIdDto student {  get; set; } = new NameIdDto();
        public StudentGradesBeforFinal Grades {  get; set; } = new StudentGradesBeforFinal();

    }
}
