using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfStudent { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public Level Level { get; set; }
        public int LevelId { get; set; }
        public List<TeacherSubjectClass> TeacherSubjectClasses { get; set; }

    }
}
