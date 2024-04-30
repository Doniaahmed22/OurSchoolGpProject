using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class SubjectLevel
    {
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }

        public Level Level { get; set; }
        public int LevelId { get; set; }
    }
}
