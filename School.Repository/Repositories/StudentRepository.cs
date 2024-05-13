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
        public async Task<IEnumerable<StudentSubject>> GetStudentsWithGradesInSubjectbyClassId(int classid , int subjectid)
        {
             return _context.StudentSubjects.Include(ss => ss.student)
                .Where(ss => ss.student.ClassId == classid && ss.SubjectId == subjectid).ToList();
        }

        //public async GetGradesOfSubjectOfStudents



    }
}
