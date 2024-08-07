﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.GradesDto;
using School.Services.Dtos.SharedDto;
using School.Services.Dtos.StudentDto;
using System.Xml.Linq;

namespace School.Services.Services.StudentServices
{
    public class StudentServices : IStudentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISubjectRecordRepository subjectRecordRepository;
        private readonly IGenericRepository<SchoolInfo> SchoolInfo;
        private readonly IStudentRepository studentRepository;
        private readonly IGradeRepository gradeRepository;

        public StudentServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            subjectRecordRepository = (SubjectRecordRepository)_unitOfWork.repository<SubjectLevelDepartmentTerm>();
            SchoolInfo = _unitOfWork.repository<SchoolInfo>();
            studentRepository = (StudentRepository)_unitOfWork.repository<Student>();
            gradeRepository = (GradeRepository)_unitOfWork.repository<StudentSubject>();
        }

    public async Task<IEnumerable<StudentDtoWithId>> GetAllStudents()
        {
            var students = await _unitOfWork.repository<Student>().GetAll();
            return _mapper.Map<IEnumerable<StudentDtoWithId>>(students);
        }

        public async Task<StudentProfile> GetStudentProfile(int id)
        {
            StudentProfile dto = new StudentProfile();
            var student = await studentRepository.GetStudentById(id);
            dto.Id = student.Id;    
            dto.Name = student.Name;
            dto.Address = student.Address;
            dto.PhoneNumber = student.PhoneNumber;
            dto.Email = student.Email;
            dto.BirthDay = student.BirthDay;
            dto.Gender = student.Gender;
            dto.Age= student.Age;
            dto.ClassNumber = student.Class.Number;
            dto.ClassId = student.ClassId;
            dto.LevelName = student.Level.Name;
            //dto.LevelNumber = student.Level.LevelNumber;
            dto.LevelId = student.LevelId;
            dto.DepartmentId = student.DepartmentId;
            dto.DepartmentName = student.Department.Name;
            dto.Religion = student.Religion;
            dto.Nationality = student.Nationality;
            return dto;
        }
        
        public async Task<StudentDtoWithId> GetStudentById(int id)
        {
            var student = await _unitOfWork.repository<Student>().GetById(id);
            return _mapper.Map<StudentDtoWithId>(student);
        }

        public async Task AddStudent(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);

            await _unitOfWork.repository<Student>().Add(student);
            if (student.LevelId != 0 && student.DepartmentId != 0)
            {
                await SetSubjectStudentRecords(student.Id, student.LevelId, student.DepartmentId);
            }
        }
        public async Task SetSubjectStudentRecords(int StuId, int levelId, int departmentId)
        {
            IEnumerable<SchoolInfo> schools = await _unitOfWork.repository<SchoolInfo>().GetAll();
            int currentterm = schools.ToList()[0].CurrentTerm;
            IEnumerable<Subject> subjects = subjectRecordRepository.GetSubjectsByLevelDeptTerm(levelId, departmentId, currentterm).ToList();
            foreach (Subject subject in subjects)
            {
                await gradeRepository.Add(new StudentSubject { StudentId = StuId, SubjectId = subject.Id });
            }

        }

        public async Task UpdateStudent(int id,StudentDto studentDto)
        {
            var existingStudent = await _unitOfWork.repository<Student>().GetById(id);

            if (existingStudent == null)
            {
                throw new InvalidOperationException("Student not found");
            }
            /* /////////////////////////////////Ask TA
            if(studentDto.LevelId != null && studentDto.DepartmentId != null)
            {
               if( existingStudent.LevelId != studentDto.LevelId.Value&& existingStudent.DepartmentId != studentDto.DepartmentId.Value)
                
                    await SetSubjectStudentGrades(id, studentDto.LevelId.Value, studentDto.DepartmentId.Value);         
            }
            else if (studentDto.LevelId != null)
            {
                if (existingStudent.LevelId != studentDto.LevelId.Value && existingStudent.DepartmentId != studentDto.DepartmentId.Value)

                    await SetSubjectStudentGrades(id, studentDto.LevelId.Value, studentDto.DepartmentId.Value);
            }
            */

            await _unitOfWork.repository<Student>().Update( existingStudent);

        }

        public async Task DeleteStudent(int id)
        {
            await _unitOfWork.repository<Student>().Delete(id);

        }

        public async Task<IEnumerable<NameIdDto>> GetStudentsByClassId(int ClassId)
        {
            List<NameIdDto> studentsDtos = new List<NameIdDto>();
            IEnumerable<Student> Students = await studentRepository.GetStudentsByClassId(ClassId);
            foreach (Student student in Students)
            {
                studentsDtos.Add(new NameIdDto()
                {
                    Id = student.Id,
                    Name = student.Name,
                });
            }
            return studentsDtos;
        }

        public async Task<IEnumerable<AbsentDaysDto>> GetStudentsWithAbsentDays()
        {
            List<AbsentDaysDto> StudentDtos = new List<AbsentDaysDto>();
            var students = await studentRepository.GetStudentsWithAbsentDays();
            foreach (var student in students)
            {
                AbsentDaysDto studentWithAbs = new AbsentDaysDto();
                studentWithAbs.StudnetId = student.student.Id;
                studentWithAbs.StudentName = student.student.Name;
                studentWithAbs.LevelNum = student.student.Level.LevelNumber;
                studentWithAbs.DepartmentName = student.student.Department.Name;
                studentWithAbs.ClassNum = student.student.Class.Number;

                studentWithAbs.AbsentDays = student.AbsentDays;
                studentWithAbs.AbsenceWarning = student.AbsenceWarning;
                StudentDtos.Add(studentWithAbs);
            }
            return StudentDtos;
        }
        public async Task<IEnumerable<StudentWithParentDto>> GetStudentsWithParentByClassID(int ClassId, string studentname = null)
        {
            List<StudentWithParentDto> studentsDtos = new List<StudentWithParentDto>();
            IEnumerable<Student> Students;

            if (studentname == null)
                Students = await studentRepository.GetStudentsWithParentByClassID(ClassId);
            else
                Students = await studentRepository.SeacrhStudentsByClassIDStudentName(ClassId, studentname);

            if (Students == null)
                return null;
            foreach (Student student in Students)
            {
                StudentWithParentDto s = new StudentWithParentDto();

                s.StudentId = student.Id;
                s.StudentName = student.Name;
                if (student.Parent != null)
                {
                    s.ParentPhoneNumber = student.Parent.PhoneNumber;
                    s.ParentName = student.Parent.Name;
                    s.ParentId = student.Parent.Id;
                }

                studentsDtos.Add(s);
            }
            return studentsDtos;
        }
        public async Task< IEnumerable<StudentWithParentAllDto>> GetStudentsWithParent()
        {
            List<StudentWithParentAllDto>dtoList = new List<StudentWithParentAllDto>();
            IEnumerable<Student> students= await studentRepository.GetStudentsWithParent();
            foreach (Student student in students)
            {
                StudentWithParentAllDto dto = new StudentWithParentAllDto();
                dto.StudentId = student.Id;
                dto.StudentName = student.Name;
                dto.LevelNumber = student.Level.LevelNumber;
                dto.DepartmentName = student.Department.Name;
                if(student.Class !=null)
                    dto.ClassNumber = student.Class.Number;
                if(student.Parent != null)
                {
                    dto.ParentPhoneNumber = student.Parent.PhoneNumber;
                    dto.ParentName = student.Parent.Name;   
                    dto.ParentId = student.Parent.Id;
                }
                if(student.requestMeetings != null) 
                    dto.NumberOfRequestMeeting = student.requestMeetings.Count;
                else
                    dto.NumberOfRequestMeeting = 0;
                dtoList.Add(dto);
            }
            return dtoList;
        }

    }
}






        