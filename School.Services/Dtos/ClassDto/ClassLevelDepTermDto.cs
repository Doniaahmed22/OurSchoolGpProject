using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class ClassLevelDepTermDto
    {
        public int Number { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }

        public int TermId {  get; set; } 
    }
}
