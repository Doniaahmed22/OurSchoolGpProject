using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.SubjectRecord
{
    public class SubjectRecordUpdateDto:SubjectRecordAddUpdateDto
    {
        public int SubLevlDeptTermId { get; set; }

    }
}
