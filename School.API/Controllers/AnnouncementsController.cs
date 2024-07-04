using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Services.Dtos.AnnouncementDto;
using School.Services.Services.AnnouncementService;

namespace School.API.Controllers
{

    [ApiController]
    public class AnnouncementsController : ControllerBase
    {

        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }



        [HttpGet]
        [Route("api/GetStudentsAnnouncements")]
        public async Task<ActionResult<IEnumerable<GetAnnouncements>>> GetStudentsAnnouncements(int id)
        {
            if(id == 0 )
            {
                return BadRequest("id musn't be zero");
            }
            if (id == null)
            {
                return BadRequest("id musn't be empty");
            }

            var announcements = await _announcementService.GetStudentsAnnouncementsAsync(id);
            return Ok(announcements);
        }


        [HttpGet]
        [Route("api/GetParentssAnnouncements")]
        public async Task<ActionResult<IEnumerable<CreateSchoolDto>>> GetParentssAnnouncements()
        {
            var announcements = await _announcementService.GetParentsAnnouncementsAsync();
            return Ok(announcements);
        }


        [HttpGet]
        [Route("api/GetTeachersAnnouncements")]
        public async Task<ActionResult<IEnumerable<CreateSchoolDto>>> GetTeachersAnnouncements()
        {
            var announcements = await _announcementService.GetTeachersAnnouncementsAsync();
            return Ok(announcements);
        }


        [HttpPost]
        [Route("api/SchoolPostAnnouncement")]
        public async Task<ActionResult> SchoolPostAnnouncement(CreateSchoolDto createSchoolDto)
        {
            if (createSchoolDto.Title == "" || createSchoolDto.Title == null)
            {
                return BadRequest("Title musn't be empty");
            }

            if (createSchoolDto.Message == "" || createSchoolDto.Message == null)
            {
                return BadRequest("Message musn't be empty");
            }

            if (createSchoolDto.ForWhich != 1 && createSchoolDto.ForWhich != 2 && createSchoolDto.ForWhich != 3 && createSchoolDto.ForWhich != 4)
            {
                return BadRequest("in ForWhich you must choose 1 for Student or 2 for Parent or 3 for Teacher or 4 for All of them");
            }

            await _announcementService.CreateSchoolAnnouncementAsync(createSchoolDto);
            return Ok("Announcement Added Successfully"); 
                //CreatedAtAction(nameof(GetAnnouncements), new { id = createdAnnouncement.Id }, createdAnnouncement);
        }


        [HttpPost]
        [Route("api/TeacherCreateAnnouncement")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDto dto)
        {
            if (dto.Title == "" || dto.Title == null)
            {
                return BadRequest("Title musn't be empty");
            }

            if (dto.Message == "" || dto.Message == null)
            {
                return BadRequest("Message musn't be empty");
            }

            if (dto.Subjects == "" || dto.Subjects == null)
            {
                return BadRequest("Subjects musn't be empty");
            }

            if (dto.ClassIds == null )
            {
                return BadRequest("You must choose class or more");
            }

            var announcement = await _announcementService.CreateAnnouncementAsync(dto);
            return Ok("Announcement Added Successfully");
        }


        [HttpGet("GetAllAnnouncements")]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var announcements = await _announcementService.GetAllAnnouncementsAsync();
            return Ok(announcements);
        }

        [HttpGet("GetTeacherSubjectsAsync")]
        public async Task<IActionResult> GetTeacherSubjectsAsync(int teacherId)
        {
            var announcements = await _announcementService.GetTeacherSubjectsAsync(teacherId);
            return Ok(announcements);
        }

        [HttpGet("GetTeacherClassesBasedOnSubjectAsync")]
        public async Task<IActionResult> GetTeacherClassesBasedOnSubjectAsync(int teacherId, int subjectId)
        {
            var Classes = await _announcementService.GetTeacherClassesBasedOnSubjectAsync(teacherId, subjectId);
            return Ok(Classes);
        }



    }
}
