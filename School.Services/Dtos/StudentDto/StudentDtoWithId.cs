using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.StudentDto
{
    public class StudentDtoWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
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
        public Level? Level { get; set; }
        public int? LevelId { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public Class? Class { get; set; }
        public int? ClassId { get; set; }
        public List<StudentSubject>? StudentSubjects { get; set; }
        public List<Attendence>? Attendences { get; set; }
    }
}
