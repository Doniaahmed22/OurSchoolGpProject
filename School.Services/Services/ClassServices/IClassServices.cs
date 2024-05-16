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
        IEnumerable<ClassDtoWithId> GetAllClasses();
        Task<ClassDtoWithId> GetClassById(int id);
        Task<ClassAllTeachersWithSubjectDto> AssignTeachersInClass(int classId);
        Task<ClassWithTeacher_Subject> ClassDetaialsTeacherWithSubject(int id);

        Task<Class> AddClass(ClassAddUpdateDto classDto);
        Task<Class> UpdateClass(int id, ClassAddUpdateDto classDto);
        Task<Class> DeleteClass(int id);
        Task<IEnumerable<NameIdDto>> GetSubjectsByClassTeacher(int classid, int teacherid);

    }
}

