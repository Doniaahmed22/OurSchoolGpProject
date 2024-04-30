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
        public List<SubjectTerm> SubjectTerms { get; set; }
        
        public List<SubjectDepartment> SubjectDepartments { get; set; }
        public List<StudentSubject> StudentSubjects { get; set; }
        public List<SubjectLevel> SubjectLevels { get; set; }
        public List<TeacherSubjectLevel> TeacherSubjectLevels { get; set; }
        public List<TeacherSubjectClass> TeacherSubjectClasses { get; set; }




    }
}
