﻿using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.Attendance
{
    public class StudentAttendanceReportDto
    {
        public int AbsentDays { get; set; }
        public int AbsenceWarns {  get; set; }
        public List<DateWithAttendanceType> Attendance { get; set; } = new List<DateWithAttendanceType>();

    }
}
