using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.StudentDto
{
    public class AbsentDaysDto
    {
        public int StudnetId { get; set; }
        public string StudentName { get; set; }
        public int LevelNum { get; set; }
        public string DepartmentName { get; set; }
        public int ClassNum { get; set; }
        public int AbsenceWarning { get; set; }
        public int AbsentDays { get; set; }
    }
}
