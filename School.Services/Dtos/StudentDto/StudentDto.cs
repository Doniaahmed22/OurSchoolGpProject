using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.StudentDto
{
    public class StudentDto
    {
        public string Name { get; set; }
        public string Religion { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Nationality { get; set; }
        public Char Gender { get; set; }
        public int? ParentId { get; set; }
        public int? LevelId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ClassId { get; set; }
    }
}
