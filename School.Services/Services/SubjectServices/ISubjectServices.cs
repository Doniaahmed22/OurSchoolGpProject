using Microsoft.AspNetCore.Http;
using School.Data.Entities;
using School.Services.Dtos.SharedDto;
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
        Task<IEnumerable<SubjectDtoWithId>> GetAllSubject();
        Task<SubjectDtoWithId> GetSubjectById(int id);
        Task<IEnumerable<SubIdNameImg>> GetSubjectsByStudId(int StudId);

        Task AddSubject(IFormFile image, SubjectDto SubDto);
        Task<Subject> DeleteSubject(int id);
        Task<Subject> UpdateSubject(int id, SubjectDto SubDto);
        Task<IEnumerable<NameIdDto>> GetSubjectsByClassTeacher(int classid, int teacherid);

    }
}
