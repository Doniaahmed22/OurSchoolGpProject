using School.Services.Dtos.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.AttendanceService
{
    public interface IAttendanceService
    {
        Task TakeAttendance(IEnumerable<StudentIdPresentDto> StudentsAttendance , int TeacherId);
        Task<AbsenceStudent> RemoveAbsenceWarn(int studentid);
        Task AddLimitAbsentDays(int LimitAbsentDays);
        Task<StudentAttendanceReportDto> GetStudenceAttendanceReport(int studentId);


    }
}
