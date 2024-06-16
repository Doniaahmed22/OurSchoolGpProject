using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Data.Entities;
using School.Services.Dtos.MaterialDto;
using School.Services.Services.FileService;
using School.Services.Services.MaterialService;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService; 

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [HttpPost("uploadMaterial/{MaterialType:int}")]
        public async Task<IActionResult> UploadMaterial(IFormFile material , MaterialType MaterialType,[FromForm]MaterialAddDto dto)
        {
            if (material == null)
            {
                return BadRequest(new {msg="no material was sended"});
            }
            try
            {
                string filepath = await _materialService.UploadMaterial(material, MaterialType, dto);
                if (string.IsNullOrEmpty(filepath))
                {
                    return Conflict("A file with the same name already exists or failed to save.");
                }
                return Ok(new {filname= Path.GetFileName(filepath) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a 400 Bad Request status with the exception message

            }


        }

    }
}
