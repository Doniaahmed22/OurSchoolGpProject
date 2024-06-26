using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Repositories
{
    public class RequestMeetingRepository:GenericRepository<RequestMeeting>,IRequestMeetingRepository
    {
        public RequestMeetingRepository(SchoolDbContext context):base(context) { }

        public async Task<RequestMeeting> GetMeetingByStudenIdDate(int studendId , DateTime date)
        {
            return await _context.requestMeetings.FirstOrDefaultAsync(r => r.StudentId == studendId && r.Date == date);
        }
        public async Task<IEnumerable< DateTime>> GetRequestMeetingDates(int studendId)
        {
            return await _context.requestMeetings.Where(r => r.StudentId == studendId)
                .Select(r=>r.Date).OrderByDescending(r => r.Date)
                .ToListAsync();
        }
    }
}
