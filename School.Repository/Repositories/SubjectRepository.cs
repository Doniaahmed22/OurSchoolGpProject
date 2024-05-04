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
              return _context.SubjectLevelDepartmentTerms;
        }
        public  SubjectLevelDepartmentTerm GetSubjectwithTermLevelDeptById(int id)
        {
          return _context.SubjectLevelDepartmentTerms.FirstOrDefault(s=>s.Id == id);
           // return  SubjectLevelDepartmentTerms.FirstOrDefault(s => s.Id == id);

        }
        public async Task<int> GetSubjectIdByName(string SubjectName)
        {

           Subject subject=  await _context.Subjects.FirstOrDefaultAsync(s => s.Name == SubjectName);
            if (subject == null)
                return -1;
            return subject.Id;
        }

        public async Task AddSubLevelDepTerm(SubjectLevelDepartmentTerm SubRecord)
        {
            _context.SubjectLevelDepartmentTerms.Add(SubRecord);
            await _context.SaveChangesAsync();
        }
    }
}
