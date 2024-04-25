using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{   //attendence
    public class Attendence
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public bool IsAttendence {  get; set; }
        public DateTime Date { get; set; }
    }
}
