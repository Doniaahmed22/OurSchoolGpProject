using School.Data.Entities;
using School.Services.Dtos.SubjectRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.SubjectRecord
{
    public interface ISubjectRecordServices
    {
        IEnumerable<SubjectRecordDto> GetAllRecords();
        Task<SubjectRecordDto> GetRecordById(int id);
         Task AddRecord(Dtos.SubjectRecord.SubjectRecordAddUpdateDto dto);


         Task<SubjectLevelDepartmentTerm> UpdateRecord(int id, SubjectRecordAddUpdateDto record);


         Task<SubjectLevelDepartmentTerm> DeleteRecord(int id);
    }
}
