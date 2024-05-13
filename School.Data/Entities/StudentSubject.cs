using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class StudentSubject:BaseEntity
    {
        public Student student { get; set; }
        public int StudentId { get; set; }
        public Subject subject { get; set; }
        public int SubjectId { get; set; }
        public int MidTerm { get; set; }
        public int Final { get; set; }
        public int WorkYear { get; set; }
    }
}
