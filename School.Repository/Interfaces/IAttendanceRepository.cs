using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IAttendanceRepository : IGenericRepository<Attendance>
    {
        Task<Attendance> GetAttendanceRecord(int studentid, DateTime date);
        Task<AbsenceWarning> GetAbsencWarningRecord(int studentid, DateTime date);
        void RemoveAbsencWarningRecord(AbsenceWarning warning);
        Task<AbsenceWarning> GetFirstAbsenceWarnByStuId(int studentid);
        Task<IEnumerable<Attendance>> GetAttendanceRecordTillDate(int studentid, DateTime date);
        void RemoveAttendanceRecord(Attendance attendance);
         int GetAbsentDaysByStudentId(int stuId);
        int GetAbsenWarnByStudentId(int stuId);
        Task<IEnumerable<Attendance>> GetAttendance(int stuId);

        Task<IEnumerable<AbsenceWarning>> GetAbsenceWarning(int stuId);

    }
}
