using School.Data.Entities.Identity;
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
 //       public string UserId { get; set; }
 //       public AppUser User { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string GmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public Char Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Degree { get; set; }

        public List<Attendance> Attendences { get; set; }
        public List<TeacherSubject> TeacherSubject { get; set; }
        public List<ClassTeacherSubjectDto> TeacherSubjectClasses { get; set; }

        public string UserId { get; set; }
        //public AppUser AppUser { get; set; }
    }
}
