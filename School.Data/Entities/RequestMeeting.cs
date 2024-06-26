using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class RequestMeeting:BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
