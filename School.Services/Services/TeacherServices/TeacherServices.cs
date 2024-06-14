using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.SharedDto;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.TeacherDto;
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

        public TeacherServices (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;   
            teacherRepository = (TeacherRepository)_unitOfWork.repository<Teacher>();
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
        public async Task<GetAllDto> GetAll()
        {
            GetAllDto Dto = new GetAllDto();


            var subjects =await _unitOfWork.repository<Subject>().GetAll();
            foreach(var subject in subjects) {
                Dto.Subjects.Add(new NameIdDto()
                {
                    Name = subject.Name
                    ,
                    Id = subject.Id
                });
            }
            Dto.teachers = await GetTeachers();
            return Dto ;
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
        public async Task AddTeacher(AddTeacherDto teacherDto)
        {
           var teacher=  new Teacher();
           List< TeacherSubject >teacherSubjects = new List<TeacherSubject> ();
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
