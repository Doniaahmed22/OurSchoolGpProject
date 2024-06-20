using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class TeacherSubjectDto
    {
        public NameIdDto Teacher {  get; set; } = new NameIdDto();

        public NameIdDto Subject { get; set; } = new NameIdDto();

    }
}
