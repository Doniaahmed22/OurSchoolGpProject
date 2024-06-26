using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Data.Entities.ProgressReport;
using School.Services.Dtos.ProgressReportDto;
using School.Services.Services.ProgressReportService;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressReportController : ControllerBase
    {
        private readonly IProgressReportService _progressReportService;
        public ProgressReportController(IProgressReportService progressReportService)
        {
            _progressReportService = progressReportService;
        }

        [HttpGet("GetReport/{StudendId:int}/{SubjectId:int}")]
        public async Task<IActionResult> GetReport(int StudendId,int SubjectId)
        {
            ReportDto report= await _progressReportService.GetReportByStuIdSubjId(StudendId, SubjectId);
            return Ok(report);
        }

        [HttpPost("AddReport")]
        public async Task<IActionResult>AddReport(ReportWithoutDateDto dto)
        {
            await _progressReportService.AddProgressRepor(dto);
            return Ok();
        }

    }
}
