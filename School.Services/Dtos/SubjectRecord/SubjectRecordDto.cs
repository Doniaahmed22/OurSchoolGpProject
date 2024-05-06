using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Services.Dtos.SharedDto;

namespace School.Services.Dtos.SubjectRecord
{
    public class SubjectRecordDto
    {
        public int SubLevlDeptTermId { get; set; }
        public NameIdDto Subject { get; set; } = new NameIdDto();
        public NameIdDto Term { get; set; } = new NameIdDto();
        public NameIdDto Department { get; set; } = new NameIdDto();
        public NameIdDto Level { get; set; } = new NameIdDto();
    }
}
