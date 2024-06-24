using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{   //attendence
    public class Attendance:BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public AttendanceType AttendanceType {  get; set; }
        public DateTime Date { get; set; }
    }
}
