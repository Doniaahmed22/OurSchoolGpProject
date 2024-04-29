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
        Task<IEnumerable<ParentDto>> GetAllParents();
        Task<ParentDto> GetParentById(int id);
        Task AddParent(ParentDto parentDto);
        Task UpdateParent(ParentDto entity);
        Task DeleteParent(int id);
    }
}
