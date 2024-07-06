using School.Services.Dtos.ParentStudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ParentStudent
{
    public interface IAddParentStudent
    {
        Task AddParentAndStudent(ParentStudentDto parentStudentdto);
    }
}
