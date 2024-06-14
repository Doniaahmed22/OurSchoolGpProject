using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.SubjectDto;
using School.Services.Dtos.SubjectRecord;
using School.Services.Services.SubjectRecord;
using School.Services.Services.SubjectServices;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectRecordController : ControllerBase
    {
        private ISubjectRecordServices subjectRecordServices;
        public SubjectRecordController(ISubjectRecordServices subjectRecordServices)
        {
            this.subjectRecordServices = subjectRecordServices;
        }

        [HttpGet]
        public async Task<ActionResult<SubjectRecordGetAll> >GetAllRecords() //
        {
            var records =  await subjectRecordServices.GetAllRecords();
            return Ok(records);
        }
        [HttpGet("{id:int}")]
        public async Task< ActionResult<SubjectRecordDto>> GetRecordById(int id)
        {
            
            var record = await subjectRecordServices.GetRecordById(id);
            if(record == null) 
                return NotFound();
            return Ok(record);
        }
        [HttpGet("Search/{name:alpha}")]
        public async Task<ActionResult<IEnumerable<SubjectRecordDto>>> GetRecordById(string name)
        {

            var records = await subjectRecordServices.SearchBySubjectName(name);
            if (records == null)
                return NotFound();
            return Ok(records);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(SubjectRecordAddUpdateDto recordDto)
        {
            if (recordDto == null)
                return BadRequest("record is empty");
            await subjectRecordServices.AddRecord(recordDto);
            return Ok();
        }

        [HttpPut("Upadate/{id:int}")]
        public async Task<IActionResult> Update(int id, SubjectRecordAddUpdateDto recordDto)
        {
            var sub = await subjectRecordServices.UpdateRecord(id, recordDto);
            if (sub == null)
                return NotFound();
            return Ok();
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await subjectRecordServices.DeleteRecord(id);
            if (subject == null)
                return NotFound();
            return Ok();
        }
    }
}
