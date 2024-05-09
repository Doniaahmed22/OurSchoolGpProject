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
    public class ClassRepository:GenericRepository<Class>, IClassRepository
    {
       // private readonly ISubjectRecordRepository SubjectRecordRepository;
        public ClassRepository(SchoolDbContext context) : base(context)//, ISubjectRecordRepository SubjectRecordRepository
        {
           // this.SubjectRecordRepository = SubjectRecordRepository;
        }

        public IEnumerable<Class> GetAllClasses()
        {
            return _context.Classes.Include(c => c.Department)
                .Include(c => c.Level);

        }
        public async Task<Class> GetClassById(int id)
        {
            return await _context.Classes.Include(c => c.Department)
                .Include(c => c.Level).FirstOrDefaultAsync(c=>c.Id==id);
        }
        public async Task<Class> ClassDetaialsTeacherWithSubject(int id)
        {
            return await _context.Classes.Include(c=>c.Level).Include(c=>c.Department).Include(c => c.TeacherSubjectClasses)
               .ThenInclude(r => r.Teacher).Include(c => c.TeacherSubjectClasses).ThenInclude(r => r.Subject).FirstOrDefaultAsync(c => c.Id == id);
                                   
        }
/*
        public async ClassGetAssignTeacher Task<Class> (int id)
        {
            var c = await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);

        }*/

    }
}
