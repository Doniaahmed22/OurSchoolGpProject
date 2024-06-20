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
        public  SubjectController (ISubjectServices subjectServices)
        {
            SubjectServices = subjectServices;
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
        public async Task<IActionResult> Add(IFormFile? image,[FromForm]SubjectDto SubjectDto)
        {
            if(SubjectDto == null)
                return BadRequest("subject is empty");
            await SubjectServices.AddSubject(image,SubjectDto);
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
        //no idea of that
        [HttpGet("GetSubjectsByClassTeacher/{classid}/{teacherid}")]
        public async Task<IActionResult> GetSubjectsByClassTeacher(int classid, int teacherid)
        {
            var subjects = await SubjectServices.GetSubjectsByClassTeacher(classid, teacherid);
            return Ok(subjects);
        }
    }
    
}
