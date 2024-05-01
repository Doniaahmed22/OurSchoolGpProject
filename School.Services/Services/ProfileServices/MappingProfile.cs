using AutoMapper;
using School.Data.Entities;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.TeacherDto;

namespace School.Services.Services.ProfileServices
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, AddStudentDto>();
            CreateMap<AddStudentDto, Student>();             
            CreateMap<Student, StudentDtoWithId>();
            CreateMap<StudentDtoWithId, Student>();

            CreateMap<Parent, ParentDto>();
            CreateMap<ParentDto, Parent>();
            CreateMap<Parent, ParentDtoWithId>();
            CreateMap<ParentDtoWithId, Parent>();

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto,Teacher>();
            CreateMap<Teacher, TeacherDtoWithId>();


            // Other mappings
        }
    }
}
