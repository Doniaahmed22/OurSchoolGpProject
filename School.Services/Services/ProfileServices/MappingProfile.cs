using AutoMapper;
using School.Data.Entities;
using School.Services.Dtos.ClassDto;
using School.Services.Dtos.ClassRecord;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.SubjectDto;
using School.Services.Dtos.SubjectRecord;
using School.Services.Dtos.TeacherDto;

namespace School.Services.Services.ProfileServices
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, Dtos.StudentDto.StudentDto>();
            CreateMap<Dtos.StudentDto.StudentDto, Student>();             
            CreateMap<Student, StudentDtoWithId>();
            CreateMap<StudentDtoWithId, Student>();

            CreateMap<Parent, ParentDto>();
            CreateMap<ParentDto, Parent>();
            CreateMap<Parent, ParentDtoWithId>();
            CreateMap<ParentDtoWithId, Parent>();

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto,Teacher>();
            CreateMap<Teacher, TeacherDtoWithId>();

            CreateMap<Subject,SubjectDto>();
            CreateMap<SubjectDto, Subject>();

            CreateMap<Subject, SubjectDtoWithId>();
            CreateMap<SubjectDtoWithId, Subject>();

            CreateMap<SubjectLevelDepartmentTerm, Dtos.SubjectRecord.SubjectRecordAddUpdateDto>();
            CreateMap<Dtos.SubjectRecord.SubjectRecordAddUpdateDto, SubjectLevelDepartmentTerm>();

            CreateMap < Class, ClassAddUpdateDto>();
            CreateMap<ClassAddUpdateDto, Class>();

            CreateMap<TeacherSubjectClass, ClassRecordDto>();
            CreateMap<ClassRecordDto, TeacherSubjectClass>();
            // CreateMap<SubjectLevelDepartmentTerm, SubjectRecordDto>();
            // Other mappings
        }
    }
}
