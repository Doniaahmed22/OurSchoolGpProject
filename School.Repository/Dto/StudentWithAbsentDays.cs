using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Dto
{
    public class StudentWithAbsentDays
    {
        public Student student {  get; set; }
        public int AbsentDays { get; set; }
        public int AbsenceWarning {  get; set; }
    }
}
