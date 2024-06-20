using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Class:BaseEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int NumOfStudent { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public Level Level { get; set; }
        public int LevelId { get; set; }
        public IEnumerable<TeacherSubjectClass> TeacherSubjectClasses { get; set; }
        public List<ClassMaterial> ClassMaterials { get; set; }

    }
}
