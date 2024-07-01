using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Dto;
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


        public async Task<IEnumerable< Student>> GetStudentsByClassId(int ClassId)
        {
            return await _context.Students.Where(s => s.ClassId == ClassId).OrderBy(s=>s.Name).ToListAsync();
        }

        public async Task<IEnumerable< StudentWithAbsentDays>> GetStudentsWithAbsentDays()
        {
            return await _context.Students
                .Include(s => s.Attendences).Include(s=>s.AbsenceWarnings)
                .Include(s=>s.Level).Include(s=>s.Department).Include(s=>s.Class)
                .Select(s => new StudentWithAbsentDays
                {
                    student = s,
                    AbsentDays = s.Attendences.Count(a => a.AttendanceType==AttendanceType.Absent),
                    AbsenceWarning = s.AbsenceWarnings.Count
                })
                .OrderByDescending(s=>s.AbsenceWarning).ThenByDescending(s=>s.AbsentDays)
                .ToListAsync();

        }
        public async Task<Student> GetStudentWithAttendanceById(int stuId)
        {
            return await _context.Students.Include(s => s.Attendences)
                        .FirstOrDefaultAsync(s => s.Id == stuId);
           
        }
        public async Task<IEnumerable<Student>> GetStudentsWithParentByClassID(int ClassId)
        {
            return await _context.Students.Include(s=>s.Parent)
                .Where(s => s.ClassId == ClassId).OrderBy(s => s.Name).ToListAsync();
        }
        public async Task<IEnumerable<Student>> SeacrhStudentsByClassIDStudentName(int ClassId , string studentName)
        {
            return await _context.Students.Include(s => s.Parent)
                .Where(s => s.ClassId == ClassId&&s.Name.Contains(studentName)).OrderBy(s => s.Name).ToListAsync();
        }
        public async Task<IEnumerable<Student>> GetStudentsWithParent()
        {
            return await _context.Students.Include(s=>s.Parent)
                .Include(s=>s.Level).Include(s=>s.Department).Include(s=>s.requestMeetings)
                .Include(s=>s.Class).OrderBy(s => s.Name).ToListAsync();

        }
        public async Task<IEnumerable<Student>> GetStudentsByLevelIdDepartmentId(int levelId, int departmentId)
        {
            return await _context.Students.Include(s => s.StudentSubjects)
                .Where(s => s.LevelId == levelId && s.DepartmentId == departmentId).ToListAsync();

        }
        public async Task<IEnumerable<Student>> GetStudentsWithAbsenceAttendance_Warns()
        {
            return await _context.Students.Include(s=>s.AbsenceWarnings).Include(s=>s.Attendences).ToListAsync();
        }

        public async Task<int> GetStudentByUserId(string UserId)
        {
            var student = await _context.Students.Where(x => x.UserId == UserId).ToListAsync();
            return student[0].Id;
        }


    }
}
