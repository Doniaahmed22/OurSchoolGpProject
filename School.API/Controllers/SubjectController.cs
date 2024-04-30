using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Dtos.SubjectDto;
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
        public IActionResult GetAll() {
            var subjects = SubjectServices.GetAllSubject();
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
        public async Task<IActionResult> Add(SubjectDtoAddUpdate SubjectDto)
        {
            if(SubjectDto == null)
                return BadRequest("subject is empty");
            await SubjectServices.AddSubject(SubjectDto);
            return Ok();
        }
/*
        [HttpPost("Upadate/{id:int}")]
        public async Task<IActionResult> Upadate(int id,SubjectDtoAddUpdate SubjectDto)
        {

            return Ok();
        }
*/
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject= await SubjectServices.DeleteSubject(id);
            if (subject == null)
                return NotFound();
            return Ok();
        }
    }
}
