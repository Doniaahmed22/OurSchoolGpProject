using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IGradeRepository:IGenericRepository<StudentSubject>
    {
        Task<IEnumerable<StudentSubject>> GetStudentsWithGradesInSubjectbyClassId(int classid, int subjectid);
        Task<IEnumerable<StudentSubject>> GetStudentsGradesByName(int classid, int subjectid, string name);

        Task<StudentSubject> GetStuGradesBySubjectId(int StudId, int subjectId);
        Task<IEnumerable<StudentSubject>> GetStudentGradesByStuId(int id);

    }
}
