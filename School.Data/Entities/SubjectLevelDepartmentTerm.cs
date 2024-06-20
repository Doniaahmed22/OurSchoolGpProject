using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class SubjectLevelDepartmentTerm:BaseEntity
    {
        
        public int Id;
        public int SubjectId { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }
        public int TermId { get; set; }
        public Subject Subject { get; set; }
        public Level Level { get; set; }
        public Term Term { get; set; }
        public Department Department { get; set; }
        

    }
}
