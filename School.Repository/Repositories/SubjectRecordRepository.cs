using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Repositories
{
    public class SubjectRecordRepository:GenericRepository<SubjectLevelDepartmentTerm> , ISubjectRecordRepository
    {
        public  SubjectRecordRepository (SchoolDbContext context):base(context)
        {

        }

        public IEnumerable<SubjectLevelDepartmentTerm> GetAllRecord()
        {
            return _context.SubjectLevelDepartmentTerms.Include(r=>r.Subject)
                .Include(r => r.Level)
                .Include(r => r.Department).Include(r => r.Term);
                 
        }
        public async Task<SubjectLevelDepartmentTerm> GetRecordById(int id)
        {
            return await _context.SubjectLevelDepartmentTerms.Include(r => r.Subject)
                .Include(r => r.Level)
                .Include(r => r.Department).Include(r => r.Term).FirstOrDefaultAsync(r=>r.Id==id);
        }

    }
}
