using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.SubjectDto
{
    public class SubjectDtoAddUpdate
    {
        public int SubjectId {  get; set; }
        public int TermId { get; set; }
        public int DepatmentId { get; set; }
        public int LevelId { get; set; }

    }
}
