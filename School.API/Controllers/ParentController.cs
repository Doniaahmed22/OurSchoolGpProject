using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.StudentDto;
using School.Services.Services.ParentServices;
using School.Services.Services.StudentServices;

namespace School.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentServices _parentServices;

        public ParentController(IParentServices parentServices)
        {
            _parentServices = parentServices;
        }

        [HttpGet]
        [Route("GetParents")]
        public async Task<ActionResult<IEnumerable<ParentDtoWithId>>> GetParents()
        {
            var Parents = await _parentServices.GetAllParents();
            return Ok(Parents);
        }

        [HttpGet]
        [Route("GetParentById/{id}")]
        public async Task<ActionResult<ParentDtoWithId>> GetParentById(int id)
        {
            var parent = await _parentServices.GetParentById(id);
            if (parent == null)
            {
                return NotFound();
            }
            return Ok(parent);
        }

        [HttpPost]
        [Route("AddParent")]
        public async Task<IActionResult> AddParent(ParentDto parentDto)
        {
            if (parentDto == null)
            {
                return BadRequest("Parent is Empty");
            }
            await _parentServices.AddParent(parentDto);
            return Ok();
        }


        [HttpPut]
        [Route("UpdateParent")]
        public async Task<IActionResult> UpdateParent(int id, ParentDto parentDto)
        {
            await _parentServices.UpdateParent(id, parentDto);
            return Ok();
        }

        [HttpDelete("DeleteParent/{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            await _parentServices.DeleteParent(id);
            return Ok();
        }

    }
}


