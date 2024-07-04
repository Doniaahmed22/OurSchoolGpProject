using School.Data.Entities;
using School.Services.Dtos.SharedDto;
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
        IEnumerable<SubLevelImage> GetTeacherSubjectsInLevel(int teacherid, int Levelid =0);
        IEnumerable<NameNumberId> GetTeacherLevels(int teacherid);
        Task<IEnumerable<NumIdDto>> GetTeacherClassesByLevelSubAsync(int teacherid, int levelid, int subjectid);

        Task AddTeacher(AddTeacherDto teacherDto);
        Task<Teacher> UpdateTeacher(int id, AddTeacherDto teacherDto);
        Task<Teacher> DeleteTeacher(int id);
        Task<IEnumerable<TecherClassInfo>> GetTeachersOfStudent(int Studentid);

    }
}
