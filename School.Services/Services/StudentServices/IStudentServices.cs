using School.Services.Dtos.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.StudentServices
{
    public interface IStudentServices
    {
        Task<IEnumerable<StudentDtoWithId>> GetAllStudents();
        Task<StudentDtoWithId> GetStudentById(int id);
        Task AddStudent(AddStudentDto studentDto);
        Task UpdateStudent(int id,AddStudentDto entity);
        Task DeleteStudent(int id);
    }
}
