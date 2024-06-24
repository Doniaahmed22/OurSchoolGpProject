using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.Attendance;
using School.Services.Dtos.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.AttendanceService
{
    public class AttendanceService : IAttendanceService
    {
        readonly private IUnitOfWork _unitOfWork;
        readonly private IStudentRepository _studentRepository;
        readonly private  ISchoolRepository _schoolRepository;
        readonly private IAttendanceRepository _attendanceRepository;

        public AttendanceService(IUnitOfWork _unitOfWork,IStudentRepository studentRepository,IAttendanceRepository _attendanceRepository, ISchoolRepository schoolRepository) {
            this._unitOfWork = _unitOfWork;
            this._studentRepository = studentRepository;
            this._schoolRepository = schoolRepository;
            this._attendanceRepository = _attendanceRepository;
        }

        public async Task TakeAttendance(IEnumerable<StudentIdPresentDto> StudentsAttendance, int TeacherId)
        {
            int limitAbsentAttendance = _schoolRepository.GetLimitAbsentDays();
            foreach (var studentAtten in StudentsAttendance)
            {
               
                if (studentAtten.AttendanceType==AttendanceType.None)
                    continue;

                var AttendanceRecord =await _attendanceRepository.GetAttendanceRecord(studentAtten.StudentId, DateTime.Today);
                if(AttendanceRecord == null) {
                    _unitOfWork.repository<Attendance>().AddWithoutSave(new Attendance()
                    {
                        Date = DateTime.Today,
                        StudentId = studentAtten.StudentId,
                        AttendanceType = studentAtten.AttendanceType,
                        TeacherId = TeacherId
                    });
                }
                else
                {
                    if (studentAtten.AttendanceType == AttendanceRecord.AttendanceType)
                        continue;

                    if (studentAtten.AttendanceType == AttendanceType.Present && AttendanceRecord.AttendanceType == AttendanceType.Absent)
                    {
                        var AbsencWarningRecord = await _attendanceRepository.GetAbsencWarningRecord(studentAtten.StudentId,DateTime.Today);
                        if (AbsencWarningRecord != null)
                            _attendanceRepository.RemoveAbsencWarningRecord(AbsencWarningRecord);
                    }           
                    AttendanceRecord.AttendanceType = studentAtten.AttendanceType;
                }

                if (studentAtten.AttendanceType== AttendanceType.Absent) {
                    var student =  await _studentRepository.GetStudentWithAttendanceById(studentAtten.StudentId);   
                    if (student != null)
                    {
                        int NumberOfAbsentDays =  student.Attendences.Count(a=>a.AttendanceType== AttendanceType.Absent);
                        if(NumberOfAbsentDays% limitAbsentAttendance == 0)
                        {
                            _unitOfWork.repository<AbsenceWarning>().
                             AddWithoutSave(new AbsenceWarning()
                             {
                                 StudentId = studentAtten.StudentId,
                                 WarningDate = DateTime.Today
                             }); 
                        }
                    }
                }
            }
            await _unitOfWork.CompleteAsync();
        }
       public async Task<AbsenceStudent> RemoveAbsenceWarn(int studentid)
       {
           var AbsenceWarn= await _attendanceRepository.GetFirstAbsenceWarnByStuId(studentid);
            if (AbsenceWarn == null)
                return null;
            IEnumerable<Attendance> attendances = await _attendanceRepository.GetAttendanceRecordTillDate(studentid, AbsenceWarn.WarningDate);
            foreach(Attendance attendance in attendances)
            {
                if(attendance.AttendanceType == AttendanceType.Absent) 
                    attendance.AttendanceType = AttendanceType.AbsenceExcuse;
            }
            _attendanceRepository.RemoveAbsencWarningRecord(AbsenceWarn);
            await _unitOfWork.CompleteAsync();
            return new AbsenceStudent()
            {
                AbsenceWarns = _attendanceRepository.GetAbsenWarnByStudentId(studentid),
                AbsentDays = _attendanceRepository.GetAbsentDaysByStudentId(studentid),
                Studentid = studentid,
            };
       }
       public async Task AddLimitAbsentDays(int LimitAbsentDays)
        {
            await _schoolRepository.SetLimitAbsentDays(LimitAbsentDays);
        }
        public async Task< StudentAttendanceReportDto >GetStudenceAttendanceReport(int studentId)
        {
            IEnumerable<Attendance> attendances = await _attendanceRepository.GetAttendance(studentId);
            IEnumerable<AbsenceWarning>absenceWarnings = await _attendanceRepository.GetAbsenceWarning(studentId);
            StudentAttendanceReportDto dto = new StudentAttendanceReportDto();

            dto.AbsenceWarns = absenceWarnings.Count();
            if (attendances == null)
                return dto;
            dto.AbsentDays = attendances.Count(a=>a.AttendanceType==AttendanceType.Absent); 
            attendances = attendances.Take(20);
            foreach (Attendance attendance in attendances)
            {
                dto.Attendance.Add(new DateWithAttendanceType() { AttendanceType = attendance.AttendanceType, date = attendance.Date });
            }
            return dto;

        }
    }
}
