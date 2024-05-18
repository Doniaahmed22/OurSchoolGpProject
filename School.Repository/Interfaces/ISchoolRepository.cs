using School.Data.Entities;
using School.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface ISchoolRepository:IGenericRepository<SchoolInfo>
    {
        Task<Term> GetCurrentTerm();

    }
}
