using School.Services.Dtos.ParentDto;
using School.Services.Dtos.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ParentServices
{
    public interface IParentServices
    {
        Task<IEnumerable<SubjectDto>> GetAllParents();
        Task<SubjectDto> GetParentById(int id);
        Task AddParent(SubjectDto parentDto);
        Task UpdateParent(int id ,SubjectDto entity);
        Task DeleteParent(int id);
    }
}
