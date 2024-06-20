using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassRecord
{
    public class ClassRecordDto
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId {  get; set; }
    }
}
