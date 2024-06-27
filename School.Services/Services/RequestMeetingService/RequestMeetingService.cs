using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.RequestMeetingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.RequestMeetingService
{
    public class RequestMeetingService:IRequestMeetingService
    {
        private readonly IRequestMeetingRepository _requestMeetingRepo;
        public RequestMeetingService(IRequestMeetingRepository requestMeetingRepo) {
            _requestMeetingRepo = requestMeetingRepo;
        }

        public async Task AddRequestMeeting(int StudentId ) {
            DateTime TodayDate = DateTime.Today;
            RequestMeeting request = await _requestMeetingRepo.GetMeetingByStudenIdDate(StudentId, TodayDate);
            if(request == null)
            {
                RequestMeeting NewRequestMeeting = new RequestMeeting()
                {
                    StudentId = StudentId,
                    Date = TodayDate
                };
                await _requestMeetingRepo.Add(NewRequestMeeting);
            }
            else
            {
                request.Date = TodayDate;
                await _requestMeetingRepo.Update(request);
            }
        }
        public async Task<IEnumerable<DateTime>>GetRequestMeetingDates(int studentid)
        {
            List<DateTime> dtos = new List<DateTime>();
            IEnumerable<DateTime> dates = await _requestMeetingRepo.GetRequestMeetingDates(studentid);
            foreach( DateTime date in dates)
            {
                dtos.Add(date);
            }
            return dtos;

        }
    }
}
