using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Services.Dtos.ClassDto;
using School.Services.Dtos.SharedDto;
using School.Services.Services.ClassServices;

namespace School.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassServices classServices;
       // private readonly IClassRecordServices ClassRecordServices;
        public ClassController(IClassServices classServices)//, IClassRecordServices ClassRecordServices
        {
            this.classServices = classServices;
            //this.ClassRecordServices = ClassRecordServices;
        }

        [HttpGet]
        [Route("GetAllClasses")]
        public async Task< IActionResult> GetAllClasses()
        {
            var classes = await classServices.GetAllClasses();
            return Ok(classes);
        }
/*
        [HttpGet]
        [Route("GetClassById/{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var classItem = await classServices.GetClassById(id);
            if (classItem == null)
            {
                return NotFound();
            }
            return Ok(classItem);
        }*/
        [HttpGet]
        [Route("AssignTeachers/{id:int}")]
        public async Task<IActionResult> GetAssignTeachersInClass(int id)
        {
            var classInfo = await classServices.GetAssignTeachersInClass(id);
            return Ok(classInfo);

        }
        [HttpGet("GetClassById/{id:int}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var classItem = await classServices.GetClassWithTeachersAndSubjectByClassId(id);
            if (classItem == null)
            {
                return NotFound();
            }
            return Ok(classItem);

        }
        [HttpGet("SearchByClassNum/{classnum:int}")]
        public async Task<IActionResult> GetClassByClassNum(int classnum)
        {
            var classItem = await classServices.GetClassesByClassNum(classnum);
            if (classItem == null)
            {
                return NotFound();
            }
            return Ok(classItem);

        }
        [HttpPost]
        [Route("AddClass")]
        public async Task<IActionResult> AddClass(ClassAddUpdateDto classItem)
        {
           var c= await classServices.AddClass(classItem);

            return Ok();
        }

        [HttpPut]
        [Route("UpdateClass/{id:int}")]
        public async Task<IActionResult> UpdateClass(int id,ClassAddUpdateDto classItem)
        {
            if (classItem == null || id == 0)
            {
                return BadRequest();
            }

            var existingClass = await classServices.UpdateClass(id,classItem);
            if (existingClass == null)
            {
                return NotFound();
            }
            return Ok();
        }


        [HttpPut]
        [Route("UpdateClassRecords/{id:int}")]
        public async Task <IActionResult>UpdateRecords(int id, List<TeacherSubjectUpdateClassRecordsDto> dto)
        {
            await classServices.UpdateRecords(id,dto);
            return Ok();
        }


        [HttpDelete]
        [Route("DeleteClass/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classItem = await classServices.DeleteClass(id);
            if (classItem == null)
            {
                return NotFound();
            }
            return Ok();
        }



    }
}
