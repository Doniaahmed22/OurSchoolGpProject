using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.Attendance
{
    public class AbsenceStudent
    {
        public int Studentid {  get; set; }
        public int AbsentDays { get; set; }
        public int AbsenceWarns { get; set; }
    }
}
