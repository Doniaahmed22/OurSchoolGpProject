﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetMaterialFields")]
        public async Task<IActionResult> GetMaterialFields()
        {
            var materialfields = _materialService.GetMaterialFields();
            return Ok(materialfields);
        }


        [HttpGet("GetMaterialForTeacher/{MaterialType:int}")]
        public async Task<IActionResult> GetMaterialForTeacher(MaterialType MaterialType,int teacherid ,int LevelId  ,int SubjectId )
        {
            var material = await _materialService.GetMaterialsForTeacher(MaterialType , teacherid, LevelId, SubjectId);
            return Ok(material);
        }
        [HttpGet("GetMaterialForTeacherInClass/{MaterialType:int}")]
        public async Task<IActionResult> GetMaterialForTeacherInClass(MaterialType MaterialType, int teacherid, int LevelId, int SubjectId, int classid)
        {
            var material = await _materialService.GetMaterialsForTeacher(MaterialType, teacherid, LevelId, SubjectId, classid);
            return Ok(material);
        }
        ////////
        [Authorize(Roles = "Student")]

        [HttpGet("GetMaterialForStudent/{MaterialType:int}")]
        public async Task<IActionResult> GetMaterialForStudent(MaterialType MaterialType, int SubjectId, int StudentId)
        {   
            var material = await _materialService.GetMaterialForStudent(MaterialType, SubjectId, StudentId);
            if(material == null)
                return NotFound();
            return Ok(material);
        }

        [HttpGet("DownloadMaterial/{MaterialId:int}")]
        public async Task<IActionResult> DownloadMaterial( int MaterialId)
        {
            try
            {
                var (memory, contentType, fileNameWithExtension) = await _materialService.DownloadMaterial( MaterialId);
                return File(memory, contentType, fileNameWithExtension);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [HttpPost("uploadMaterial/{MaterialType:int}")]
        public async Task<IActionResult> UploadMaterial( MaterialType MaterialType, [FromForm] MaterialAddDto dto)
        {
            if (dto.material == null)
            {
                return BadRequest(new { msg = "no material was sended" });
            }
            try
            {
                string filepath = await _materialService.UploadMaterial(dto.material, MaterialType, dto);
                if (string.IsNullOrEmpty(filepath))
                {
                    return Conflict("A file with the same name already exists or failed to save.");
                }
                return Ok(new { filname = Path.GetFileName(filepath) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a 400 Bad Request status with the exception message

            }

        }
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [HttpPost("uploadMaterial2/{MaterialType:int}")]
        public async Task<IActionResult> UploadMaterial2( MaterialType MaterialType, [FromForm] MaterialAddDto2 dto)
        {
            if (dto.material == null)
            {
                return BadRequest(new { msg = "no material was sended" });
            }
            try
            {
                string filepath = await _materialService.UploadMaterial(dto.material, MaterialType, dto);
                if (string.IsNullOrEmpty(filepath))
                {
                    return Conflict("A file with the same name already exists or failed to save.");
                }
                return Ok(new { filname = Path.GetFileName(filepath) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a 400 Bad Request status with the exception message

            }

        }


        [HttpDelete("DeleteMaterial/{MaterialId:int}")]
        public async Task<IActionResult> DeleteMaterial(int MaterialId)
        {
            try
            {
               await _materialService.DeleteMaterial(MaterialId);
                return Ok();
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }


    }
}
