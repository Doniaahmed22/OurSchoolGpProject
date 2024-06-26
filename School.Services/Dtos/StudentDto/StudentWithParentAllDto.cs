using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.StudentDto
{
    public class StudentWithParentAllDto : StudentWithParentDto
    {
        public int LevelNumber { get; set; }
        public String DepartmentName { get; set; }
        public int ClassNumber { get; set; }  
        public int NumberOfRequestMeeting { get; set; }
    }
}
