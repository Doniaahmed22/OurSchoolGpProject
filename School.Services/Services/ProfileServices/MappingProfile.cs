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

            CreateMap<Subject, SubjectDto>();
            CreateMap<SubjectDto, Subject>();
            CreateMap<Subject, SubjectDto>();
            CreateMap<SubjectDto, Subject>();

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto,Teacher>();
            CreateMap<Teacher, TeacherDtoWithId>();


            // Other mappings
        }
    }
}
