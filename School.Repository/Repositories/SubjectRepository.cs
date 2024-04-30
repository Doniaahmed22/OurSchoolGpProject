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

        public IEnumerable<Subject> GetSubjectwithTermLevelDepartment()
        {
              return _context.Subjects.Include(s=>s.SubjectTerms).
                 ThenInclude(subTerm=> subTerm.Term)
                .Include(s=>s.SubjectDepartments).ThenInclude(SubDep=>SubDep.Department)
                .Include(s=>s.SubjectLevels).ThenInclude(sublevel=>sublevel.Level);
        }
        public async Task< Subject>GetSubjectwithTermLevelDeptById(int id)
        {
          var Subjects= 
                _context.Subjects.Include(s => s.SubjectTerms)
                .ThenInclude(subTerm => subTerm.Term)
                .Include(s => s.SubjectDepartments)
                .ThenInclude(SubDep => SubDep.Department)
                .Include(s => s.SubjectLevels)
                .ThenInclude(sublevel => sublevel.Level);
            return await Subjects.FirstOrDefaultAsync(s => s.Id == id);

        }
    }
}
