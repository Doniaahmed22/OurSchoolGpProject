using School.Data.Entities;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.SubjectDto;
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
       // Task<GetAllDto> GetAll();
        Task<IEnumerable<TeacherDtoWithId>> GetTeachers(string name = "");
        Task<TeacherDtoWithId> GetTeacherById(int id);
        IEnumerable<SubLevelImage> GetTeacherSubjects(int teacherid);

        Task AddTeacher(AddTeacherDto teacherDto);
        Task<Teacher> UpdateTeacher(int id, AddTeacherDto teacherDto);
        Task<Teacher> DeleteTeacher(int id);
    }
}
