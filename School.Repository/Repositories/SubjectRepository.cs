﻿using Microsoft.EntityFrameworkCore;
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
/*
        public IEnumerable<Teacher> GetTeachersOfSubject(int SubId)
        {
            return _context.TeacherSubjects.Include(ts=>ts.Teacher).Where(st=>st.SubjectId== SubId).Select(st=>st.Teacher);
        }
*/
        public IEnumerable<Subject> GetSubjectsOfTeacher(int TeachId)
        {
            return _context.TeacherSubjects.Include(ts => ts.Subject)
                .Where(st => st.TeacherId == TeachId).Select(st => st.Subject);
        }
        public async Task<IEnumerable<Subject>> GetSubjectsByClassTeacher(int classid, int teacherid)
        {
            return await _context.TeacherSubjectClasses.Where(c => c.ClassId == classid && c.TeacherId == teacherid)
                .Include(tsc => tsc.Subject).Select(tsc => tsc.Subject).ToListAsync();

        }
        public async Task<IEnumerable<Subject>> GetSubjectsByLevelDeptTerm(int StuDepartmentId ,int StuLevelId, int TermId )
        {
            return await _context.SubjectLevelDepartmentTerms.Include(sldt=>sldt.Subject)
                  .Where(sldt => sldt.DepartmentId== StuDepartmentId && sldt.LevelId==StuLevelId&&sldt.TermId==TermId)
                  .Select(sldt => sldt.Subject).ToListAsync();
        }


    }
}
