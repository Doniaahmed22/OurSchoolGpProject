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
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            
            CreateMap<Parent, ParentDto>();
            CreateMap<ParentDto, Parent>();

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto,Teacher>();
            CreateMap<Teacher, TeacherDtoWithId>();


            // Other mappings
        }
    }
}
