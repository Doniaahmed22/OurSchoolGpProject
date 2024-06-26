using School.Data.Entities.ProgressReport;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ProgressReportDto
{
    public class ReportWithoutDateDto
    {
        public StatusType Status { get; set; }

        [Range(0, 100)]
        public int ProgressLevel { get; set; }
        public AttitudeType Attitude { get; set; }

        [Range(0, 100)]
        public int AbsenceRate { get; set; }
        public string Advantages { get; set; }
        public string Disadvantages { get; set; }
        public string Recommendations { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
    }
}
