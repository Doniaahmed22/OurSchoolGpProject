using School.Data.Entities;
using School.Services.Dtos.ClassRecord;
using School.Services.Dtos.SubjectRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ClassServices
{
    public interface IClassRecordServices
    {
        Task AddRecord(ClassRecordDto dto);
        Task<TeacherSubjectClass> UpdateRecord(int id, ClassRecordDto record);
        Task<TeacherSubjectClass> DeleteRecord(int id);

    }
}
