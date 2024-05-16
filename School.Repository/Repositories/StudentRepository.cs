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
    public class StudentRepository: GenericRepository<Student>, IStudentRepository
    {

        public StudentRepository( SchoolDbContext context) : base(context)
        {
            
            
        }
        public async Task<IEnumerable<Student>> GetStudentsFinalDegreeByLevelDepart(int levelId, int DeptId)
        {
            return await _context.Students.Include(s=>s.StudentSubjects).ThenInclude(ss=>ss.subject)
                .Where(s=>s.LevelId==levelId&&s.DepartmentId == DeptId).ToListAsync();
        }
        public async Task<IEnumerable<Student>> GetStudentsFinalGradesByName(int levelId, int DeptId, string name)
        {
            return await _context.Students.Include(s => s.StudentSubjects).ThenInclude(ss => ss.subject)
                .Where(s => s.LevelId == levelId && s.DepartmentId == DeptId&&s.Name.Contains(name)).ToListAsync();
        }
        public async Task<Student> GetStudentWithSubjectDegrees(int studentid)
        {
            return await _context.Students.Include(s => s.StudentSubjects).
                FirstOrDefaultAsync(s=>s.Id == studentid);
        }

        //public async GetGradesOfSubjectOfStudents



    }
}
