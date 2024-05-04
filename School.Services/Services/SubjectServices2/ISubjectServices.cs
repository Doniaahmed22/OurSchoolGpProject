using School.Data.Entities;
using School.Services.Dtos.SubjectDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.SubjectServices
{
    public interface ISubjectServices
    {
        IEnumerable<BaseSubjectInfoDto> GetAllSubject();
        Task<BaseSubjectInfoDto> GetSubjectRecordById(int id);
        Task<SubjectLevelDepartmentTerm> AddSubject(SubjectDtoAdd SubjectDto);
        Task<Subject> DeleteSubject(int id);

    }
}
