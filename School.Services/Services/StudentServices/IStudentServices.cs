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
        Task<IEnumerable<StudentDto>> GetAllStudents();
        Task<StudentDto> GetStudentById(int id);
        Task AddStudent(StudentDto studentDto);
        Task UpdateStudent(StudentDto entity);
        Task DeleteStudent(int id);
    }
}
