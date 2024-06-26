using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Entities.ProgressReport;
using School.Repository.Interfaces;
using School.Services.Dtos.ProgressReportDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ProgressReportService
{
    public class ProgressReportService:IProgressReportService
    {

        readonly private IProgressReportRepository _progressReportRepository;
        public ProgressReportService( IProgressReportRepository progressReportRepository)
        {
            _progressReportRepository = progressReportRepository;
        }
        public async Task<ReportDto> GetReportByStuIdSubjId(int StudentId, int SubjectId)
        {
            ProgressReport report = await _progressReportRepository.GetReportByStuIdSubjId(StudentId, SubjectId);
            ReportDto reportDto = new ReportDto();
            if (report == null)
                return reportDto;
            reportDto.AbsenceRate = report.AbsenceRate;
            reportDto.Attitude = report.Attitude;
            reportDto.Status = report.Status;
            reportDto.Recommendations = report.Recommendations;
            reportDto.Advantages = report.Advantages;
            reportDto.Disadvantages = report.Disadvantages;
            reportDto.ProgressLevel = report.ProgressLevel;
            reportDto.StudentId = report.StudentId;
            reportDto.SubjectId = report.SubjectId;
            reportDto.TeacherId = report.TeacherId;
            reportDto.UpadateDate = report.UpdatedDate; 
            return reportDto;
        }
        public async Task  AddProgressRepor(ReportWithoutDateDto reportDto)
        {
            ProgressReport exist_report = await _progressReportRepository.GetReportByStuIdSubjId(reportDto.StudentId, reportDto.SubjectId);
            if (exist_report == null)
            {
                ProgressReport report = new ProgressReport()
                {
                    AbsenceRate = reportDto.AbsenceRate,
                    Attitude = reportDto.Attitude,
                    Status = reportDto.Status,
                    Recommendations = reportDto.Recommendations,
                    Advantages = reportDto.Advantages,
                    Disadvantages = reportDto.Disadvantages,
                    ProgressLevel = reportDto.ProgressLevel,
                    StudentId = reportDto.StudentId,
                    SubjectId = reportDto.SubjectId,
                    TeacherId = reportDto.TeacherId,
                    UpdatedDate = DateTime.Today
                }; 
                await _progressReportRepository.Add(report);
            }
            else
            {
                exist_report.AbsenceRate = reportDto.AbsenceRate;
                exist_report.Attitude = reportDto.Attitude;
                exist_report.Status = reportDto.Status;
                exist_report.Recommendations = reportDto.Recommendations;
                exist_report.Advantages = reportDto.Advantages;
                exist_report.Disadvantages = reportDto.Disadvantages;
                exist_report.ProgressLevel = reportDto.ProgressLevel;
                exist_report.StudentId = reportDto.StudentId;
                exist_report.UpdatedDate = DateTime.Today;
                exist_report.TeacherId = reportDto.TeacherId;
                await _progressReportRepository.Update(exist_report);   
            }

        }

    }
}
