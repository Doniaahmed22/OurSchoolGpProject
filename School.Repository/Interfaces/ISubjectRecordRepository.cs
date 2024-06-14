using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public  interface ISubjectRecordRepository:IGenericRepository<SubjectLevelDepartmentTerm>
    {
        IEnumerable<SubjectLevelDepartmentTerm> GetAllRecord();
        IEnumerable<Subject> GetSubjectsWithTeachersByLevelDeptTerm(int LevelId, int DepartmentId, int TermId);
        IEnumerable<Subject> GetSubjectsByLevelDeptTerm(int LevelId, int DepartmentId, int TermId);
        Task<SubjectLevelDepartmentTerm> GetRecordById(int id);
        Task<IEnumerable<SubjectLevelDepartmentTerm>> GetRecordsBySubjectName(string name);


    }
}
