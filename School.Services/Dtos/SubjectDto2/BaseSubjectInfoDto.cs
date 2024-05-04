using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.SubjectDto
{
    public class BaseSubjectInfoDto
    {
        public int SubLevlDeptTermId { get; set; }
        public NameIdDto Subject { get; set; }
        public NameIdDto SubjectTerm { get; set; } 
        public NameIdDto SubjectDepartment { get; set; } 
        public NameIdDto SubjectLevel { get; set; } 
    }
}
