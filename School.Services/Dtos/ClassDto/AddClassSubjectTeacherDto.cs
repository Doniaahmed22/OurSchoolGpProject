using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class AddClassSubjectTeacherDto
    {
        public int ClassId { get; set; }
        public List<SubjectTeacherIdsDto> teacherSubjects { get; set; }= new List<SubjectTeacherIdsDto>();

    }
}
