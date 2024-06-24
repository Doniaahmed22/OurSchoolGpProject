using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.StudentDto
{
    public class StudentWithParentDto
    {
        public int StudentId { get; set; }
        public int ParentId { get; set; }
        public string StudentName { get; set; }
        public string ParentName { get; set; }
        public string ParentPhoneNumber { get; set; }

    }
}
