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
       //private  IGenericRepository<Subject> SubjectRepository  ;
        public  SubjectRecordRepository (SchoolDbContext context  ):base(context)
        {
           // this.SubjectRepository = new GenericRepository<Subject>(context);
        }

        public IEnumerable<SubjectLevelDepartmentTerm> GetAllRecord()
        {
            return _context.SubjectLevelDepartmentTerms.Include(r=>r.Subject)
                .Include(r => r.Level)
                .Include(r => r.Department).Include(r => r.Term);
                 
        }

        public IEnumerable<Subject> GetSubjectsWithTeachersByLevelDeptTerm(int LevelId , int DepartmentId,int TermId )
        {
            var query= _context.SubjectLevelDepartmentTerms.Include(r => r.Subject)
                .ThenInclude(s => s.TeachersSubject).ThenInclude(ts => ts.Teacher);
            return query.Where(r => r.LevelId == LevelId && r.DepartmentId == DepartmentId && r.TermId == TermId).Select(r => r.Subject);

        }
        public IEnumerable<Subject> GetSubjectsByLevelDeptTerm(int LevelId, int DepartmentId, int TermId)
        {
            return _context.SubjectLevelDepartmentTerms.Include(r => r.Subject)
                        .Where(r => r.LevelId == LevelId && r.DepartmentId == DepartmentId && r.TermId == TermId).Select(r => r.Subject);

        }
        public async Task<SubjectLevelDepartmentTerm> GetRecordById(int id)
        {
            return await _context.SubjectLevelDepartmentTerms.Include(r => r.Subject)
                .Include(r => r.Level)
                .Include(r => r.Department).Include(r => r.Term).FirstOrDefaultAsync(r=>r.Id==id);
        }
        public async Task< IEnumerable<SubjectLevelDepartmentTerm>> GetRecordsBySubjectName(string name)
        {
            return await _context.SubjectLevelDepartmentTerms.Include(r => r.Subject)
                .Include(r => r.Level)
                .Include(r => r.Department).Include(r => r.Term).Where(r=>r.Subject.Name.Contains(name)).ToListAsync();
        }

    }
}
