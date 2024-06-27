using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IRequestMeetingRepository:IGenericRepository<RequestMeeting>
    {
        Task<RequestMeeting> GetMeetingByStudenIdDate(int studendId, DateTime date);
        Task<IEnumerable<DateTime>> GetRequestMeetingDates(int studendId);

    }
}
