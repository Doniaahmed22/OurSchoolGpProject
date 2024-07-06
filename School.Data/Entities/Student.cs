using School.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Student : BaseEntity
    {
        public int Id { get; set; }
       public string UserId { get; set; }
        //public AppUser User { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string GmailAddress { get; set; }
        public string Religion { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Nationality { get; set; }
        public Char Gender { get; set; }
        public Parent? Parent { get; set; }
        public int? ParentId { get; set; }
        public Level Level { get; set; }
        [Required]
        public int LevelId { get; set; }
        public Department Department { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public Class? Class { get; set; }
        public int? ClassId { get; set; }
        public List<StudentSubject>? StudentSubjects { get; set; }
        public List<Attendance>? Attendences { get; set; }
        // public int NumbOfAttendanceWarnings { get; set; } = 0;
        public List<AbsenceWarning>AbsenceWarnings { get; set; }
        public List<RequestMeeting> requestMeetings {  get; set; }


    }
}
