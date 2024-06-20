/*
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Dtos.ClassDto;
using School.Services.Dtos.ClassRecord;
using School.Services.Dtos.SubjectRecord;
using School.Services.Services.ClassServices;
using School.Services.Services.SubjectRecord;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRecordController : ControllerBase
    {
        private IClassRecordServices ClassRecordServices;
        public ClassRecordController(IClassRecordServices ClassRecordServices)
        {
            this.ClassRecordServices = ClassRecordServices;
        }
        //[HttpGet]
        //public ActionResult<IEnumerable<SubjectRecordDto>> GetAllRecords()
        //{
        //    var records = subjectRecordServices.GetAllRecords();
        //    return Ok(records);
        //}
        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<SubjectRecordDto>> GetRecordById(int id)
        //{

        //    var record = await subjectRecordServices.GetRecordById(id);
        //    if (record == null)
        //        return NotFound();
        //    return Ok(record);
        //}
        /*
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddClassSubjectTeacherDto recordsDto)
        {
            if (recordsDto == null)
                return BadRequest("record is empty");
            await ClassRecordServices.AddRecords(recordsDto);
            return Ok();
        }*/
/*

        [HttpPut("Upadate/{id:int}")]
        public async Task<IActionResult> Update(int id, ClassRecordDto recordDto)
        {
            var sub = await ClassRecordServices.UpdateRecord(id, recordDto);
            if (sub == null)
                return NotFound();
            return Ok();
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await ClassRecordServices.DeleteRecord(id);
            if (subject == null)
                return NotFound();
            return Ok();
        }
    }
}
*/