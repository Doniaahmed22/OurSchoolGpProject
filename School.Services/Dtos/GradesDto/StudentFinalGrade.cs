using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.GradesDto
{
    public class StudentFinalGrade
    {
        public NameIdDto Student { get; set; } = new NameIdDto();
        public List<SubjectFinalDegree> subjectsFinalDegree { get; set; } = new List<SubjectFinalDegree>();
    }
}
