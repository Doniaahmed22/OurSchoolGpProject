using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.Attendance
{
    public class DateWithAttendanceType
    {
       public DateTime date {  get; set; }
        public AttendanceType AttendanceType { get; set; }
    }
}
