using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.ParentStudentDto;
using School.Services.Dtos.ParentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Services.Services.StudentServices;

namespace School.Services.Services.ParentStudent
{
    public class AddParentStudent : IAddParentStudent
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentServices _studentServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IParentRepository _parentRepository;

        public AddParentStudent(IUnitOfWork unitOfWork, IMapper mapper, IParentRepository parentRepository, IStudentRepository studentRepository, IStudentServices studentServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _parentRepository = parentRepository;
            _studentRepository = studentRepository;
            _studentServices = studentServices;
        }

        public async Task AddParentAndStudent (ParentStudentDto parentStudentdto)
        {
            Parent parentDto = new Parent();
            parentDto.Name = parentStudentdto.ParentName;
            parentDto.GmailAddress = parentStudentdto.ParentGmailAddress;
            parentDto.Address = parentStudentdto.ParentAddress;
            parentDto.PhoneNumber = parentStudentdto.ParentPhoneNumber;
            parentDto.Email = parentStudentdto.ParentEmail;
            parentDto.UserId = parentStudentdto.ParentUserId;

            await _unitOfWork.repository<Parent>().Add(parentDto);


            Student studentDto = new Student();
            studentDto.Name = parentStudentdto.StudentName;
            studentDto.Address = parentStudentdto.StudentAddress;
            studentDto.Age = parentStudentdto.StudentAge;
            studentDto.PhoneNumber = parentStudentdto.StudentPhoneNumber;
            studentDto.Religion = parentStudentdto.StudentReligion;
            studentDto.Nationality = parentStudentdto.StudentNationality;
            studentDto.BirthDay = parentStudentdto.StudentBirthDay;
            studentDto.Email = parentStudentdto.StudentEmail;
            studentDto.UserId = parentStudentdto.StudentUserId;
            studentDto.Gender = parentStudentdto.StudentGender;
            studentDto.GmailAddress = parentStudentdto.StudentGmailAddress;
            studentDto.ClassId = parentStudentdto.StudentClassId;
            studentDto.DepartmentId = parentStudentdto.StudentDepartmentId;
            studentDto.LevelId = parentStudentdto.StudentLevelId;
            studentDto.ParentId = parentDto.Id;

            await _unitOfWork.repository<Student>().Add(studentDto);
            if (studentDto.LevelId != 0 && studentDto.DepartmentId != 0)
            {
                await _studentServices.SetSubjectStudentRecords(studentDto.Id, studentDto.LevelId, studentDto.DepartmentId);
            }
        }

    }
}
