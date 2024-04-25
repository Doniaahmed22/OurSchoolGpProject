using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;

namespace School.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public ParentController(SchoolDbContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        [Route("GetParents")]
        public async Task<IActionResult> GetParents()
        {
            return Ok(await _context.Parents.ToListAsync());
        }

        [HttpGet]
        [Route("GetParentById/{id}")]
        public async Task<IActionResult> GetParentById(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return Ok(parent);
        }

        [HttpPost]
        [Route("AddParent")]
        public async Task<IActionResult> AddParent(Parent parent)
        {
            _context.Add(parent);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateParent")]
        public async Task<IActionResult> UpdateParent(Parent parent)
        {
            if (parent == null || parent.Id == 0)
                return BadRequest();

            var Newparent = await _context.Parents.FindAsync(parent.Id);
            if (Newparent == null)
                return NotFound();
            Newparent.Name = parent.Name;
            Newparent.PhoneNumber = parent.PhoneNumber;
            Newparent.Address = parent.Address;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("DeleteParent/{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            if (id < 1 || id == null)
                return BadRequest();
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
                return NotFound();
            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }


}


