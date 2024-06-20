using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.SubjectRecord
{
    public class SubjectRecordAddUpdateDto
    {
        public int SubjectId { get; set; } 
        public int TermId { get; set; } 
        public int DepartmentId { get; set; } 
        public int LevelId { get; set; } 
    }
}
