using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface ISubjectRepository:IGenericRepository<Subject>
    {
        IEnumerable<SubjectLevelDepartmentTerm> GetSubjectLevelDepartmentTerm();
        SubjectLevelDepartmentTerm  GetSubjectwithTermLevelDeptById(int id);
        Task<int> GetSubjectIdByName(string SubjectName);
        Task AddSubLevelDepTerm(SubjectLevelDepartmentTerm SubRecord);
    }
}
