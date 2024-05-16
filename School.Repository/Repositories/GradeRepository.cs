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
    public class GradeRepository : GenericRepository<StudentSubject>, IGradeRepository
    {
        public GradeRepository(SchoolDbContext context) : base(context)
        {


        }
        public async Task<IEnumerable<StudentSubject>> GetStudentsWithGradesInSubjectbyClassId(int classid, int subjectid)
        {
            return _context.StudentSubjects.Include(ss => ss.student)
               .Where(ss => ss.student.ClassId == classid && ss.SubjectId == subjectid).ToList();
        }
        public async Task<IEnumerable<StudentSubject>> GetStudentsGradesByName(int classid, int subjectid,string name)
        {
            return _context.StudentSubjects.Include(ss => ss.student)
               .Where(ss => ss.student.ClassId == classid && ss.SubjectId == subjectid&&ss.student.Name.Contains(name))
               .ToList();
        }
        public async Task<StudentSubject> GetStuGradesBySubjectId(int StudId, int subjectId)
        {
            return await _context.StudentSubjects.FirstOrDefaultAsync(sc=>sc.StudentId==StudId&&sc.SubjectId==subjectId);

        }
        public async Task<IEnumerable< StudentSubject>>GetStudentGradesByStuId(int id)
        {
            return await _context.StudentSubjects.Include(ss=>ss.subject).Where(ss=>ss.StudentId==id).ToListAsync();

        }


        /*
        public async Task<StudentSubject> GetStuGradesByName(int ClassId, int SubjId , string name )
        {
            return await _context.StudentSubjects.FirstOrDefaultAsync(sc => sc.StudentId == StudId && sc.SubjectId == subjectId);

        }*/
    }
}
