using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.StudentDto
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string GmailAddress { get; set; }
        public string Religion { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public Char Gender { get; set; }
        public string LevelName { get; set; }
        //public int LevelNumber { get; set; }
        public int LevelId{ get; set; }

        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public int? ClassNumber { get; set; }
        public int? ClassId { get; set; }


    }
}
