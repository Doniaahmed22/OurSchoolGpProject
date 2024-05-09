using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class ClassWithTeacher_Subject:ClassDtoWithId
    {
        public List<TeacherSubjectDto> TeachersWithSubject {  get; set; } = new List<TeacherSubjectDto>();
    }
}
