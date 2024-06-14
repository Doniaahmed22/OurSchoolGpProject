using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.SubjectRecord
{
    public class SubjectRecordGetAll
    {
        public List<SubjectRecordDto>subjectRecords { get; set; } = new List<SubjectRecordDto>();
        public List<NameIdDto>Terms { get; set; } = new List<NameIdDto>();
        public List<NameIdDto> Levels { get; set; } = new List<NameIdDto>();

        public List<NameIdDto> Departments { get; set; } = new List<NameIdDto>();
        public List<NameIdDto> Subjects { get; set; } = new List<NameIdDto>();

    }
}
