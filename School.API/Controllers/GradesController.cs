using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.GradesDto;
using School.Services.Services.GradeService;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeServices _gradeServices;
        private readonly ISchoolRepository _schoolRepository;
        public GradesController(IGradeServices gradeServices , ISchoolRepository schoolRepository)
        {
            _gradeServices = gradeServices;
            _schoolRepository = schoolRepository;
        }
        [HttpGet("{classid:int}/{subjectid:int}")]
        public async Task<ActionResult<IEnumerable<StudentGradeDto>>> GetStudentsWithGrades(int classid, int subjectid)
        {
            var students = await _gradeServices.GetStudentsWithGradesInSubjectbyClassId(classid, subjectid, "");/////////////////////////////
            return Ok(students);
        }
        [HttpPut("updateGrade/{studentid:int}/{subjectid:int}")]
        public async Task<ActionResult> UpdateStudentDegreeInSubject
            (int studentid, int subjectid, StudentGradesBeforFinal dto)
        {
            var errors = await _gradeServices.validateStudentDegreeInSubject(subjectid, dto);
            if(errors.Count>0)
                return BadRequest(errors);

            var studentgrade = await _gradeServices.UpdateStudentDegreeInSubject(studentid, subjectid, dto);
            if (studentgrade == null)
                return NotFound();
            return Ok();
        }

        [HttpGet("GetGradesByName/{classid:int}/{subjectid:int}/{name}")]
        public async Task<ActionResult> GetStudentsGradesByName(int classid, int subjectid, string name)
        {
            var students = await _gradeServices.GetStudentsWithGradesInSubjectbyClassId(classid, subjectid, name);
            return Ok(students);
        }
        [HttpGet("GetLevelsDepartments")]
        public async Task<ActionResult> GetLevelsDepartments()
        {
            var LevelsDepartments =await _gradeServices.GetLevelsDepartments();
            return Ok(LevelsDepartments);
        }
        [HttpGet("GetFinalDegrees/{levelid:int}/{departmentid:int}")]
        public async Task<ActionResult>GetFinalDegrees(int levelid , int departmentid)
        {
            var students = await _gradeServices.GetStudentsFinalGrades(levelid, departmentid);
            return Ok(students);
        }
        [HttpPut("UpdateFinalDegree/{StudentId:int}")]
        public async Task<ActionResult> UpdateFinalDegreeStudentId(int StudentId, List<SubjectFinalId> SubjectFinalIds)
        {
            var errors =await  _gradeServices.validateFinalGradeForSubjects(SubjectFinalIds);
            if(errors.Count > 0) 
                return BadRequest(errors);
            var student = await _gradeServices.UpdateFinalDegreeStudentId(StudentId, SubjectFinalIds);
            return Ok();
        }

        [HttpGet("GetFinalDegrees/{levelid:int}/{departmentid:int}/{Studentname}")]
        public async Task<ActionResult> GetFinalDegrees(int levelid, int departmentid, string Studentname)
        {
            var students = await _gradeServices.GetStudentsFinalGrades(levelid, departmentid, Studentname);
            return Ok(students);
        }
        [HttpGet("GetStudentDegrees/{Studentid:int}")]
        public async Task<ActionResult> GetStudentGradesByStuId(int Studentid)
        {
           var studentgrades = await _gradeServices.GetStudentGradesByStuId(Studentid);
            return Ok( studentgrades);
        }


    }
}
