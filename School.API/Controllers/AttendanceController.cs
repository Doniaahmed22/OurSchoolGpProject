using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Dtos.Attendance;
using School.Services.Services.AttendanceService;
using School.Services.Services.ClassServices;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService _attendanceService) {
            this._attendanceService = _attendanceService;
        }

        [HttpPost("TakeAttendance/{TeacherId}")]
        public async Task<IActionResult> TakeAttendance(IEnumerable<StudentIdPresentDto>StudentsAttendance,int TeacherId)
        {
            await _attendanceService.TakeAttendance(StudentsAttendance, TeacherId);
            return Ok();
        }
        [HttpDelete("RemoveAbsenceWarn/{StudendId}")]
        public async Task<IActionResult> RemoveAbsenceWarn( int StudendId)
        {
            var studentAbsence = await _attendanceService.RemoveAbsenceWarn(StudendId);
            if(studentAbsence== null)
                return NotFound();
            return Ok(studentAbsence);
        }
        [HttpPost("AddLimitAbsentDays/{LimitAbsentDays}")]
        public async Task<IActionResult> AddLimitAbsentDays(int LimitAbsentDays)
        {
            await _attendanceService.AddLimitAbsentDays(LimitAbsentDays);
            return Ok();
        }
        [HttpGet("GetStudenceAttendanceReport/{StudId:int}")]
        public async Task<IActionResult> GetStudenceAttendanceReport(int StudId)
        {
            StudentAttendanceReportDto dto= await _attendanceService.GetStudenceAttendanceReport(StudId);
            return Ok(dto);
        }
    }
}
