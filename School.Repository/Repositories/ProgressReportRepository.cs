using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities.ProgressReport;
using School.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Repositories
{
    public class ProgressReportRepository:GenericRepository<ProgressReport>,IProgressReportRepository
    {
        public  ProgressReportRepository(SchoolDbContext context) : base(context) { }
        //GetFirstO
        public async Task<ProgressReport> GetReportByStuIdSubjId(int StudentId , int SubjectId)
        {
           return await _context.ProgressReport.FirstOrDefaultAsync(p=>p.StudentId==StudentId && p.SubjectId==SubjectId);

        }
  
    }
}
