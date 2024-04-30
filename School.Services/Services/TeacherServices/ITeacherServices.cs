using School.Data.Entities;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.TeacherDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.TeacherServices
{
    public interface ITeacherServices
    {
        Task<IEnumerable<TeacherDtoWithId>> GetAllTeacher();
        Task<TeacherDtoWithId> GetTeacherById(int id);
        Task AddTeacher(TeacherDto teacher);
        Task<Teacher> UpdateTeacher(int id , TeacherDto dto);
        Task<Teacher> DeleteTeacher(int id);
    }
}
