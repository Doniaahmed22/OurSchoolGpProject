using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double fees { get; set; }
        public List<TeacherLevel> TeacherLevels { get; set; }
        public List<TeacherSubjectLevel> TeacherSubjectLevels { get; set; }


    }
}
