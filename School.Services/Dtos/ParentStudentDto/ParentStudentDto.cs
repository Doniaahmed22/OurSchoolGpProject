using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ParentStudentDto
{
    public class ParentStudentDto
    {
        public string ParentName { get; set; }
        public string? ParentEmail { get; set; }
        public string ParentGmailAddress { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string? ParentAddress { get; set; }
        public string? ParentUserId { get; set; }
        public string StudentName { get; set; }
        public string? StudentEmail { get; set; }
        public string StudentGmailAddress { get; set; }
        public string StudentReligion { get; set; }
        public DateTime StudentBirthDay { get; set; }
        public int StudentAge { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string? StudentAddress { get; set; }
        public string StudentNationality { get; set; }
        public Char StudentGender { get; set; }
        public int? StudentParentId { get; set; }
        public int StudentLevelId { get; set; }
        public int StudentDepartmentId { get; set; }
        public int StudentClassId { get; set; }
        public string? StudentUserId { get; set; }

    }
}
