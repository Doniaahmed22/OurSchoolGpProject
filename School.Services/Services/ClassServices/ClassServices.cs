using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.ClassDto;
using School.Services.Dtos.SharedDto;
using School.Services.Dtos.SubjectRecord;

namespace School.Services.Services.ClassServices
{
    public class ClassServices : IClassServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRecordRepository _subjectRecordRepository;

        private readonly IMapper _mapper;

        public ClassServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _classRepository = (ClassRepository)_unitOfWork.repository<Class>();
            _subjectRecordRepository = (SubjectRecordRepository)_unitOfWork.repository<SubjectLevelDepartmentTerm>();
        }

        public IEnumerable<ClassDtoWithId> GetAllClasses()
        {
            var classes = _classRepository.GetAllClasses();
            List<ClassDtoWithId> classesDto = new List<ClassDtoWithId>();
            foreach (var c in classes)
            {
                ClassDtoWithId classDto = new ClassDtoWithId();
                classDto.Id = c.Id;
                classDto.number = c.Number;
                classDto.NumOfStudent = c.NumOfStudent;
                classDto.Level.Id = c.LevelId;
                classDto.Level.Name = c.Level.Name;
                classDto.Department.Id = c.DepartmentId;
                classDto.Department.Name = c.Department.Name;
                classesDto.Add(classDto);
            }
            return classesDto;
        }

        public async Task<ClassDtoWithId> GetClassById(int id)
        {
            var c = await _classRepository.GetClassById(id);
            if (c == null)
                return null;
            ClassDtoWithId classDto = new ClassDtoWithId();
            classDto.Id = c.Id;
            classDto.number = c.Number;
            classDto.NumOfStudent = c.NumOfStudent;
            classDto.Level.Id = c.LevelId;
            classDto.Level.Name = c.Level.Name;
            classDto.Department.Id = c.DepartmentId;
            classDto.Department.Name = c.Department.Name;
            return classDto;
         }

        public async Task AddClass(ClassAddUpdateDto classDto)
        {
            var classItem = _mapper.Map<Class>(classDto);
            await _unitOfWork.repository<Class>().Add(classItem);
        }

        public async Task<Class> UpdateClass(int id, ClassAddUpdateDto classDto)
        {
            var existingClass = await _unitOfWork.repository<Class>().GetById(id);
            if (existingClass == null)
            {
                return null;
            }

            _mapper.Map(classDto, existingClass);
            await _unitOfWork.repository<Class>().Update(existingClass);
            return existingClass;
        }

        public async Task<Class> DeleteClass(int id)
        {
            return await _unitOfWork.repository<Class>().Delete(id);
            
        }

        public async Task< ClassAllTeachersWithSubjectDto> AssignTeachersInClass(int classId)
        {
            ClassAllTeachersWithSubjectDto TeachersWithSubjectDto;
            var c = await GetClassById(classId);
            c.Term.Id = 2;
            c.Term.Name = "second Name";
            var subjectsWithTeachers = _subjectRecordRepository.GetSubjectsWithTeachersByLevelDeptTerm(c.Level.Id, c.Department.Id, c.Term.Id);
            TeachersWithSubjectDto = new ClassAllTeachersWithSubjectDto
                (c.Id, c.number, c.NumOfStudent
                , new NameIdDto() { Id = c.Department.Id, Name = c.Department.Name },
                new NameIdDto() { Id = c.Level.Id, Name = c.Level.Name },
                new NameIdDto() { Id = c.Term.Id, Name = c.Term.Name }
                );
            TeachersWithSubjectDto.PutTolist(subjectsWithTeachers);
            return TeachersWithSubjectDto;
        }
        public async Task<ClassWithTeacher_Subject> ClassDetaialsTeacherWithSubject(int id)
        {
            var Class = await _classRepository.ClassDetaialsTeacherWithSubject(id);
            if (Class == null)
                return null;
            ClassWithTeacher_Subject class_dto = new ClassWithTeacher_Subject();
            class_dto.Id = id;
            class_dto.number = Class.Number;
            class_dto.Level.Id = Class.Level.Id;
            class_dto.Level.Name = Class.Level.Name;
            class_dto.Department.Id = Class.Department.Id;
            class_dto.Department.Name = Class.Department.Name;

            foreach(var techerSub in Class.TeacherSubjectClasses)
            {
                TeacherSubjectDto teacherSubjectDto = new TeacherSubjectDto();
                teacherSubjectDto.Subject.Name = techerSub.Subject.Name;
                teacherSubjectDto.Subject.Id = techerSub.Subject.Id;
                teacherSubjectDto.Teacher.Name = techerSub.Teacher.Name;
                teacherSubjectDto.Teacher.Id = techerSub.Teacher.Id;

                class_dto.TeachersWithSubject.Add(teacherSubjectDto);

            }
            return class_dto;
        }
    }
}
