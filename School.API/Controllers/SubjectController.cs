using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Dtos.SubjectDto;
using School.Services.Services.ClassServices;
using School.Services.Services.SubjectServices;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectServices SubjectServices;
        private readonly IClassServices classServices;
        public  SubjectController (ISubjectServices subjectServices, IClassServices classServices)
        {
            SubjectServices = subjectServices;
            this.classServices = classServices;
        }

        [HttpGet("GetAll")]
        public async Task< IActionResult >GetAll() {
            var subjects = await SubjectServices.GetAllSubject();
            return Ok(subjects);
        }
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await SubjectServices.GetSubjectById(id);
            if (subject == null)
                return NotFound();
            return Ok(subject);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(SubjectDto SubjectDto)
        {
            if(SubjectDto == null)
                return BadRequest("subject is empty");
            await SubjectServices.AddSubject(SubjectDto);
            return Ok();
        }

        [HttpPut("Upadate/{id:int}")]
        public async Task<IActionResult> Update(int id,SubjectDto SubjectDto)
        {
             var sub= await SubjectServices.UpdateSubject(id,SubjectDto);
             if (sub == null)
                return  NotFound();
            return Ok();
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject= await SubjectServices.DeleteSubject(id);
            if (subject == null)
                return NotFound();
            return Ok();
        }

        [HttpGet("GetSubjectsByClassTeacher/{classid}/{teacherid}")]
        public async Task<IActionResult> GetSubjectsByClassTeacher(int classid, int teacherid)
        {
            var subjects = await classServices.GetSubjectsByClassTeacher(classid, teacherid);
            return Ok(subjects);
        }
    }
    
}
