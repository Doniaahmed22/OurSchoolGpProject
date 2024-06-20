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
    public class SubjectRepository:GenericRepository<Subject>,ISubjectRepository
    {
        public SubjectRepository(SchoolDbContext _context):base(_context) { }

        public IEnumerable<Teacher> GetTeachersOfSubject(int SubId)
        {
            return _context.TeacherSubjects.Include(ts=>ts.Teacher).Where(st=>st.SubjectId== SubId).Select(st=>st.Teacher);
        }

        public IEnumerable<Subject> GetSubjectsOfTeacher(int TeachId)
        {
            return _context.TeacherSubjects.Include(ts => ts.Subject)
                .Where(st => st.TeacherId == TeachId).Select(st => st.Subject);
        }


    }
}
