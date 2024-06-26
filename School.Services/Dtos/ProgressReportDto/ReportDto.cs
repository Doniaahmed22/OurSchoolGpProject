using School.Data.Entities.ProgressReport;
using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ProgressReportDto
{
    public class ReportDto:ReportWithoutDateDto
    {
        public DateTime UpadateDate { get; set; }


    }
}
