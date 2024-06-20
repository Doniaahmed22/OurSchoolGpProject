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
    public class TeacherRepository: GenericRepository<Teacher>,ITeacherRepository 
    {
        public TeacherRepository(SchoolDbContext context) : base(context) { }
        public IEnumerable<Teacher> GetTeachersWithSubject() {
            return _context.Teachers.Include(t => t.TeacherSubject).ThenInclude(ts => ts.Subject);
        }
        public async Task<Teacher> GetTeachersWithSubjectById(int techerid)
        {
            return await _context.Teachers.Include(t => t.TeacherSubject)
                .ThenInclude(ts => ts.Subject).FirstOrDefaultAsync(t=>t.Id== techerid);
        }
        public IEnumerable< Teacher> GetTeachersByName(string name)
        {
            return  _context.Teachers.Include(t => t.TeacherSubject)
                .ThenInclude(ts => ts.Subject).Where(t=>t.Name.Contains(name));
        }

    }
}
