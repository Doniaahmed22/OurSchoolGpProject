using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(SchoolDbContext context) : base(context)
        {
        }

        public IEnumerable<SubjectLevelDepartmentTerm> GetSubjectLevelDepartmentTerm()
        {
              return _context.SubjectLevelDepartmentTerms.Include(s=>s.Subject)
                .Include(s=>s.Level).Include(s=>s.Department).Include(s=>s.Term);
        }
        public async Task<SubjectLevelDepartmentTerm> GetSubjectwithTermLevelDeptById(int id)
        {
          var SubjectLevelDepartmentTerms = _context.SubjectLevelDepartmentTerms.Include(s => s.Subject)
                .Include(s => s.Level).Include(s => s.Department).Include(s => s.Term);
            return await SubjectLevelDepartmentTerms.FirstOrDefaultAsync(s => s.Id == id);

        }
    }
}
