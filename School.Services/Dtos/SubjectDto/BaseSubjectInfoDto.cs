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
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<NameIdDto> SubjectTerms { get; set; } = new List<NameIdDto>();
        public List<NameIdDto> SubjectDepartments { get; set; } = new List<NameIdDto>();
        public List<NameIdDto> SubjectLevels { get; set; } = new List<NameIdDto>();
    }
}
