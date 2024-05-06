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
        Task<SubjectLevelDepartmentTerm> GetRecordById(int id);

    }
}
