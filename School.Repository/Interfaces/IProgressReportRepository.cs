using School.Data.Entities.ProgressReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IProgressReportRepository:IGenericRepository<ProgressReport>
    {
        Task<ProgressReport> GetReportByStuIdSubjId(int StudentId, int SubjectId);

    }
}
