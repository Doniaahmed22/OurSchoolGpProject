using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Dto
{
    public class TeacherSubjectDto
    {
        public Teacher Teacher { get; set; }
        public string TeacherSubjectName { get; set; }
    }
}
