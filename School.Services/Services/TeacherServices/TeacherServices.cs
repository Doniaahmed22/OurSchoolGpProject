using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.SharedDto;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.SubjectDto;
using School.Services.Dtos.TeacherDto;
using School.Services.Services.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.TeacherServices
{
    public class TeacherServices : ITeacherServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeacherRepository teacherRepository;
        private readonly IFileService _fileService;
        // private readonly IClassRepository _classRepository;
        public TeacherServices (IUnitOfWork unitOfWork, IMapper mapper , IFileService fileService)//,IClassRepository classRepository)
        {
            _unitOfWork = unitOfWork;   
            teacherRepository = (TeacherRepository)_unitOfWork.repository<Teacher>();
            _fileService = fileService;
           // _classRepository = classRepository;
        }

        public async Task<IEnumerable<TeacherDtoWithId>> GetTeachers(string name="")
        {
            IEnumerable<Teacher> teachers;
            if (name=="")
                teachers= teacherRepository.GetTeachersWithSubject();
            else
                teachers = teacherRepository.GetTeachersByName(name);

            List< TeacherDtoWithId> teachersDto = new List<TeacherDtoWithId>();
            foreach (var teacher in teachers)
            {
                TeacherDtoWithId teacherDto = new TeacherDtoWithId();
                teacherDto.Id = teacher.Id;
                teacherDto.Name = teacher.Name; 
                teacherDto.GmailAddress = teacher.GmailAddress;
                teacherDto.Email = teacher.Email;
                teacherDto.BirthDay = teacher.BirthDay;
                teacherDto.PhoneNumber = teacher.PhoneNumber;
                teacherDto.Nationality = teacher.Nationality;
                teacherDto.Gender = teacher.Gender;
                teacherDto.Degree = teacher.Degree;
                teacherDto.Religion = teacher.Religion;
                foreach(TeacherSubject Teachsub in teacher.TeacherSubject)
                {
                    teacherDto.TeacherSubjects.Add(new NameIdDto()
                    { Name = Teachsub.Subject.Name, Id = Teachsub.Subject.Id });
                }
                teachersDto.Add(teacherDto);
            }
            return teachersDto ;
        }
        public async Task<TeacherDtoWithId> GetTeacherById(int id)
        {
            
            var teacher = await teacherRepository.GetTeachersWithSubjectById(id);
            if (teacher == null)
                return null;

            TeacherDtoWithId teacherDto = new TeacherDtoWithId();
            teacherDto.Id = teacher.Id;
            teacherDto.Name = teacher.Name;
            teacherDto.BirthDay = teacher.BirthDay;
            teacherDto.PhoneNumber = teacher.PhoneNumber;
            teacherDto.Nationality = teacher.Nationality;
            teacherDto.Gender = teacher.Gender;
            teacherDto.Degree = teacher.Degree;
            teacherDto.Religion = teacher.Religion;
            foreach (TeacherSubject Teachsub in teacher.TeacherSubject)
            {
                teacherDto.TeacherSubjects.Add(new NameIdDto()
                { Name = Teachsub.Subject.Name, Id = Teachsub.Subject.Id });
            }
            return teacherDto;

        }


        public IEnumerable<SubLevelImage> GetTeacherSubjectsInLevel(int teacherid , int Levelid=0)
        {
            List<SubLevelImage> dto = new List<SubLevelImage>();
            IEnumerable<TeacherSubjectClass> teacherSubjectClassRecords;
            if(Levelid == 0) 
                 teacherSubjectClassRecords = teacherRepository.GetTeacherSubjects(teacherid);
            else
                teacherSubjectClassRecords = teacherRepository.GetTeacherSubjectsInLevel(teacherid,Levelid);

            foreach (var recode in teacherSubjectClassRecords)
            {
                SubLevelImage subLevelImage = new SubLevelImage();
                string imgeName = recode.Subject.Image;
                subLevelImage.image = _fileService.GetMediaUrl(GlobalStaticService.BaseImageSubjectGet+recode.Subject.Image);

                subLevelImage.Subject.Name = recode.Subject.Name;
                subLevelImage.Subject.Id = recode.Subject.Id;
                subLevelImage.Level.num = recode.Class.Level.LevelNumber;
                subLevelImage.Level.Id = recode.Class.Level.Id;
                dto.Add(subLevelImage);
            }
            return dto;
        }
        public IEnumerable<NameNumberId> GetTeacherLevels(int teacherid)
        {
            List<NameNumberId> dto = new List<NameNumberId>();
            var Levels = teacherRepository.GetTeacherLevels(teacherid);
            foreach (var Level in Levels)
            {
                dto.Add(new NameNumberId()
                {
                    Id = Level.Id,
                    Name = Level.Name,
                    number = Level.LevelNumber
                });
            }
            dto.Add(new NameNumberId()
            {
                Id = 0,
                Name = "All Level",
            });
            return dto;
        }

        public async Task<IEnumerable<NumIdDto>> GetTeacherClassesByLevelSubAsync(int teacherid, int levelid, int subjectid)
        {
            List<NumIdDto> teacherClassesDto = new List<NumIdDto>();
            IEnumerable<Class> classes = await teacherRepository.GetTeacherClassesAsync(teacherid, levelid, subjectid);
            foreach (Class class_ in classes)
            {
                teacherClassesDto.Add(new NumIdDto()
                {
                    Id = class_.Id,
                    num = class_.Number,
                });
            }
            return teacherClassesDto;
        }
        public async Task AddTeacher(AddTeacherDto teacherDto)
        {
           var teacher=  new Teacher();
           List< TeacherSubject >teacherSubjects = new List<TeacherSubject> ();
           teacher.Name = teacherDto.Name;
           teacher.BirthDay = teacherDto.BirthDay;
            teacher.Email = teacherDto.Email;
            teacher.GmailAddress = teacherDto.GmailAddress;
            teacher.PhoneNumber = teacherDto.PhoneNumber;
           teacher.Nationality = teacherDto.Nationality;
           teacher.Gender = teacherDto.Gender;
           teacher.Degree = teacherDto.Degree;
           teacher.Religion = teacherDto.Religion;

            foreach (int SubjectId in teacherDto.TeacherSubjectsId)
                teacherSubjects.Add(new TeacherSubject() { SubjectId = SubjectId });
           teacher.TeacherSubject = teacherSubjects;
           await teacherRepository.Add(teacher);
        }

        public async Task<Teacher> UpdateTeacher(int id , AddTeacherDto teacherDto)
        {
            var teacher = await teacherRepository.GetTeachersWithSubjectById(id);
            if (teacher == null)
                return null;
            List<TeacherSubject> teacherSubjects = new List<TeacherSubject>();
            teacher.Name = teacherDto.Name;
            teacher.BirthDay = teacherDto.BirthDay;
            teacher.PhoneNumber = teacherDto.PhoneNumber;
            teacher.Nationality = teacherDto.Nationality;
            teacher.Gender = teacherDto.Gender;
            teacher.Degree = teacherDto.Degree;
            teacher.Religion = teacherDto.Religion;

            foreach (int SubjectId in teacherDto.TeacherSubjectsId)
                teacherSubjects.Add(new TeacherSubject() { SubjectId = SubjectId });
            teacher.TeacherSubject = teacherSubjects;

            await teacherRepository.Update( teacher);
            return teacher;
        }
        public async Task<Teacher> DeleteTeacher(int id)
        {
            var teacher = await teacherRepository.GetById(id);
            if (teacher == null)
                return null;
            await teacherRepository.Delete(id); 
            return teacher;
        }


    }
}
