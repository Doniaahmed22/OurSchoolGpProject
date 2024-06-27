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
            await _announcementService.CreateSchoolAnnouncementAsync(createSchoolDto);
            return Ok("Announcement Added Successfully"); 
                //CreatedAtAction(nameof(GetAnnouncements), new { id = createdAnnouncement.Id }, createdAnnouncement);
        }


        [HttpPost]
        [Route("api/TeacherCreateAnnouncement")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDto dto)
        {
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
