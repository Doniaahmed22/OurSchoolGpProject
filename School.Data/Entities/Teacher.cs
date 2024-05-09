using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Teacher:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public Char Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Degree { get; set; }

        public List<Attendence> Attendences { get; set; }
        public List<TeacherSubject> TeacherSubject { get; set; }
        public List<TeacherSubjectClass> TeacherSubjectClasses { get; set; }
    }
}
