using School.Data.Entities;
using School.Services.Dtos.SharedDto;
using School.Services.Dtos.SubjectDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.DepartmentService
{
    public interface IdepartmentService
    {
        Task<IEnumerable<NameIdDto>> GetAllDepartmentForList();

    }
}
