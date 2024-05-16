using School.Data.Entities;
using School.Services.Dtos.GradesDto;
using School.Services.Dtos.LevelDeoartmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.GradeService
{
    public interface IGradeServices
    {
        Task<List<StudentGradeDto>> GetStudentsWithGradesInSubjectbyClassId(int classid, int subjectid, string name);
        Task<StudentSubject> UpdateStudentDegreeInSubject(int studentid, int subjectid, StudentGradesBeforFinal studentGrades);
        Task<LevelsDepartmentsDto> GetLevelsDepartments();
        Task<StudentsFinalDegresDto> GetStudentsFinalGrades(int levelid, int departmentid, string name = "");

        Task<Student> UpdateFinalDegreeStudentId(int StudentId, List<SubjectFinalId> SubjectFinalIds);
        Task<List<SubjectGradesDto>> GetStudentGradesByStuId(int id);


    }
}
