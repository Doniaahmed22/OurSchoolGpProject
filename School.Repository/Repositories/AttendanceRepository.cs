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
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(SchoolDbContext context) : base(context)
        {
        }
        public async Task<Attendance> GetAttendanceRecord(int studentid, DateTime date)
        {
            return await _context.Attendences.FirstOrDefaultAsync(a => a.StudentId == studentid && a.Date == date);
        }
        public async Task<AbsenceWarning> GetAbsencWarningRecord(int studentid, DateTime date)
        {
            return await _context.AbsenceWarnings.FirstOrDefaultAsync(a => a.StudentId == studentid && a.WarningDate == date);
        }
        public void RemoveAbsencWarningRecord(AbsenceWarning warning)
        {
            _context.AbsenceWarnings.Remove(warning);

        }
        public async Task<AbsenceWarning> GetFirstAbsenceWarnByStuId(int studentid)
        {
            return await _context.AbsenceWarnings.Where(a => a.StudentId == studentid).OrderBy(a => a.WarningDate).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Attendance>> GetAttendanceRecordTillDate(int studentid, DateTime date)
        {
            return await _context.Attendences.Where(a => a.StudentId == studentid && a.Date <= date).ToListAsync();
        }
        public void RemoveAttendanceRecord(Attendance attendance)
        {
            _context.Attendences.Remove(attendance);

        }
        public int GetAbsentDaysByStudentId(int stuId)
        {
            return _context.Attendences.Count(a => a.StudentId == stuId && a.AttendanceType == AttendanceType.Absent);
        }
        public int GetAbsenWarnByStudentId(int stuId)
        {
            return _context.AbsenceWarnings.Count(a => a.StudentId == stuId);
        }
        public async Task<IEnumerable<Attendance>> GetAttendance(int stuId)
        {
            return await _context.Attendences.Where(a => a.StudentId == stuId).OrderByDescending(a => a.Date).ToListAsync();
        }
        public async Task<IEnumerable<AbsenceWarning>> GetAbsenceWarning(int stuId)
        {
            return await _context.AbsenceWarnings.Where(a => a.StudentId == stuId).ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetLastAbsencesAttendanceRecord(int studentid, int warndays)
        {
            var attendances = await _context.Attendences.Where(a => a.StudentId == studentid && a.AttendanceType == AttendanceType.Absent)
                             .OrderBy(a => a.Date)
                             .ToListAsync();
            if (attendances.Count < warndays)
                return attendances;
            else
                return attendances.Take(warndays);

        }


    }
}
