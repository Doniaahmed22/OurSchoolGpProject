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
        public IEnumerable<TeacherSubjectClass> GetTeacherSubjects(int teacherid)
        {
            return _context.TeacherSubjectClasses.Include(c => c.Class).ThenInclude(c => c.Level)
                .Include(tsc => tsc.Subject).Where(tsc => tsc.TeacherId == teacherid).AsEnumerable()
                .GroupBy(tsc => new { tsc.SubjectId, tsc.Class.Level.Id })
                .Select(g => g.First())
                .OrderBy(tsc => tsc.Class.Level.LevelNumber)
                .ToList();
        }

        public IEnumerable<Level> GetTeacherLevels(int teacherid)
        {
            return _context.TeacherSubjectClasses.Include(c => c.Class).ThenInclude(c => c.Level)
                .Where(tsc => tsc.TeacherId == teacherid).AsEnumerable()
                .GroupBy(tsc => tsc.Class.Level.Id )
                .Select(g => g.First())
                .OrderBy(tsc => tsc.Class.Level.LevelNumber).Select(s=>s.Class.Level)
                .ToList();
        }
        public IEnumerable<TeacherSubjectClass> GetTeacherSubjectsInLevel(int teacherid, int levelid)
        {
            return _context.TeacherSubjectClasses.Include(c => c.Class).ThenInclude(c => c.Level).Include(tsc=>tsc.Subject)
                .Where(tsc => tsc.TeacherId == teacherid&& tsc.Class.LevelId== levelid).AsEnumerable()
                .GroupBy(tsc => tsc.SubjectId)
                .Select(g => g.First())
                .OrderBy(tsc => tsc.Subject.Name)
                .ToList();
        }
        public async Task<IEnumerable<Class>> GetTeacherClassesAsync(int teacherid, int levelid, int subjectid)
        {
            return await _context.TeacherSubjectClasses.Include(tsc => tsc.Class)
                .Where(tsc => tsc.TeacherId == teacherid && tsc.SubjectId == subjectid && tsc.Class.LevelId == levelid).Select(tsc => tsc.Class).Distinct().ToListAsync();
        }


        public async Task<int> GetTeacherByUserId(string UserId)
        {
            var Teacher = await _context.Teachers.Where(x => x.UserId == UserId).ToListAsync();
            return Teacher[0].Id;
        }

    }
}
