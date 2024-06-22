using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.TeacherDto
{
    public class TeacherDto
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string GmailAddress { get; set; }

        public string? PhoneNumber { get; set; }
        public Char Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Degree { get; set; }
        public List<NameIdDto>TeacherSubjects { get; set; } = new List<NameIdDto>();

    }
}
