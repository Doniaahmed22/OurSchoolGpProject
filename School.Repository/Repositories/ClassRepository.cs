using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;
using System;
using System.Collections;
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
        public async Task<Class> GetClassWithTeacherSubjectClassById(int id)
        {
            return await _context.Classes.Include(c=>c.TeacherSubjectClasses)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Class> GetClassWithTeacherAndSubject(int id)
        {
            return await _context.Classes.Include(c=>c.Level).Include(c=>c.Department).Include(c => c.TeacherSubjectClasses)
               .ThenInclude(r => r.Teacher).Include(c => c.TeacherSubjectClasses).ThenInclude(r => r.Subject).FirstOrDefaultAsync(c => c.Id == id);
                                   
        }
        public async Task<IEnumerable<Class> >GetClassesbyClassNum(int classnum)
        {
            return await _context.Classes.Include(c => c.Level).Include(c => c.Department)
                .Where(c => c.Number == classnum).ToArrayAsync();//.Include(c => c.TeacherSubjectClasses)
                                                                                                                                            //.ThenInclude(r => r.Teacher).Include(c => c.TeacherSubjectClasses).ThenInclude(r => r.Subject).Where(c => c.Number == classnum).ToArrayAsync();

        }
        public async Task<IEnumerable<TeacherSubjectClass>> GetClassRecordsByClassId(int classid)
        {
            return await _context.TeacherSubjectClasses.Where(c => c.ClassId == classid).ToListAsync();

        }
        public async Task<IEnumerable<Class>> GetClassesWithTeacherLevelSubjectAsync(int teacherid , int levelid , int subjectid)
        {
            return await _context.TeacherSubjectClasses.Include(tsc => tsc.Class)
                .Where(tsc => tsc.TeacherId == teacherid && tsc.SubjectId == subjectid && tsc.Class.LevelId == levelid).Select(tsc=>tsc.Class).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByClassTeacher(int classid, int teacherid)
        {
            return await _context.TeacherSubjectClasses.Where(c => c.ClassId == classid&&c.TeacherId==teacherid)
                .Include(tsc=>tsc.Subject).Select(tsc=>tsc.Subject).ToListAsync();

        }
        /*
        public async Task<IEnumerable<Subject>> GetClassTeacherSubjectByClassId(int classid)
        {
            return await _context.TeacherSubjectClasses.Where(c => c.ClassId == classid )
                .Include(tsc => tsc.Subject).Select(tsc => tsc.Subject).ToListAsync();

        }*/


        /*
                public async ClassGetAssignTeacher Task<Class> (int id)
                {
                    var c = await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);

                }*/

    }
}
