using School.Services.Dtos.ProgressReportDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ProgressReportService
{
    public interface IProgressReportService
    {
        Task AddProgressRepor(ReportWithoutDateDto reportDto);
        Task<ReportDto> GetReportByStuIdSubjId(int StudentId, int SubjectId);

    }
}
