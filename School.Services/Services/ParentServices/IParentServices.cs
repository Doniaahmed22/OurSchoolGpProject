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
        Task<IEnumerable<ParentDtoWithId>> GetAllParents();
        Task<ParentDtoWithId> GetParentById(int id);
        Task AddParent(ParentDto parentDto);
        Task UpdateParent(int id ,ParentDto entity);
        Task DeleteParent(int id);
    }
}
