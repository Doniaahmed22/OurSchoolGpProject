using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Subject:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<StudentSubject> StudentSubjects { get; set; }
        public List<SubjectLevelDepartmentTerm> SubjeSubjectLevelDepartmentTermctLevels { get; set; }
        public List<TeacherSubject> TeachersSubject { get; set; }
        public List<TeacherSubjectClass> TeacherSubjectClasses { get; set; }

    }
}
