﻿using School.Data.Entities;
using School.Repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
        //Task<IEnumerable<StudentSubject>> GetStudentsWithGradesInSubjectbyClassId(int classid, int subjectid);
        Task<IEnumerable<Student>> GetStudentsFinalDegreeByLevelDepart(int levelId, int DeptId);
        Task<Student> GetStudentWithSubjectDegrees(int studentid);
        Task<IEnumerable<Student>> GetStudentsFinalGradesByName(int levelId, int DeptId, string name);
        Task<IEnumerable<Student>> GetStudentsByClassId(int ClassId);
        Task<IEnumerable<StudentWithAbsentDays>> GetStudentsWithAbsentDays();
       Task<Student> GetStudentWithAttendanceById(int stuId);
        Task<IEnumerable<Student>> GetStudentsWithParentByClassID(int ClassId);
        Task<IEnumerable<Student>> SeacrhStudentsByClassIDStudentName(int ClassId, string studentName);
        Task<IEnumerable<Student>> GetStudentsWithParent();
        Task<IEnumerable<Student>> GetStudentsByLevelIdDepartmentId(int levelId, int departmentId);
        Task<IEnumerable<Student>> GetStudentsWithAbsenceAttendance_Warns();
        Task<int> GetStudentByUserId(string UserId);
        Task<Student> GetStudentById(int Id);


    }
}
