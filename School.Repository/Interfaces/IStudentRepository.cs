using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
        //Task<IEnumerable<StudentSubject>> GetStudentsWithGradesInSubjectbyClassId(int classid, int subjectid);
        Task<IEnumerable<Student>> GetStudentsFinalDegreeByLevelDepart(int levelId, int DeptId);
        Task<Student> GetStudentWithSubjectDegrees(int studentid);
        Task<IEnumerable<Student>> GetStudentsFinalGradesByName(int levelId, int DeptId, string name);


    }
}
