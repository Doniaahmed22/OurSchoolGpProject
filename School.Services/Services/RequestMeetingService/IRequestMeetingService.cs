using School.Services.Dtos.RequestMeetingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.RequestMeetingService
{
    public interface IRequestMeetingService
    {
        Task AddRequestMeeting(int StudentId);
        Task<IEnumerable<DateTime>> GetRequestMeetingDates(int studentid);
    }
}
