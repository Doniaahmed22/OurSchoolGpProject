//using School.Services.Dtos.ClassDto;
using School.Data.Entities;
using School.Services.Dtos.ClassDto;
using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ClassServices
{
    public interface IClassServices
    {
        //    Task<ClassGetAllDto> GetAllClasses();
       Task<IEnumerable<ClassDtoWithId>> GetAllClasses();

        Task<ClassDtoWithId> GetClassById(int id);
        Task<IEnumerable<TeachersSubjectDto>> GetSubjectWithThierTeachers(int classId);
        Task UpdateRecords(int classid, List<TeacherWithSubjectInClass> dto);

        Task<Class> AddClass(ClassAddUpdateDto classDto);
        Task<Class> UpdateClass(int id, ClassAddUpdateDto classDto);
        Task<Class> DeleteClass(int id);
        Task<IEnumerable<ClassDtoWithId>> GetClassesByClassNum(int classnum);
        Task<IEnumerable<ClassInfoDto>> GetTeacherClasses(int TeacherId);


    }
}

