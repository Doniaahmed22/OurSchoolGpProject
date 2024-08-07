using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IGenericRepository<ClassTeacherSubjectDto> _classRecordRepoistory;
        private readonly ISchoolRepository schoolRepository;
        private readonly IMapper _mapper;

        public ClassServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _classRepository = (ClassRepository)_unitOfWork.repository<Class>();
            _subjectRecordRepository = (SubjectRecordRepository)_unitOfWork.repository<SubjectLevelDepartmentTerm>();
            _classRecordRepoistory = _unitOfWork.repository<ClassTeacherSubjectDto>();
            schoolRepository = (SchoolRepository)_unitOfWork.repository<SchoolInfo>();

        }

        public async Task< IEnumerable<ClassDtoWithId>> GetAllClasses()
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
            Term term = await schoolRepository.GetCurrentTerm();
            classDto.Id = c.Id;
            classDto.number = c.Number;
            classDto.NumOfStudent = c.NumOfStudent;
            classDto.Level.Id = c.LevelId;
            classDto.Level.Name = c.Level.Name;
            classDto.Department.Id = c.DepartmentId;
            classDto.Department.Name = c.Department.Name;
            classDto.Term.Name = term.Name;
            classDto.Term.Id = term.Id;
            return classDto;
        }/*
        public async Task<ClassWithTeacher_Subject> GetClassWithTeachersAndSubjectByClassId(int id)
        {
            var Class = await _classRepository.GetClassWithTeacherAndSubject(id);
            Term term = await schoolRepository.GetCurrentTerm();
            if (Class == null)
                return null;
            ClassWithTeacher_Subject class_dto = new ClassWithTeacher_Subject();

            class_dto.Id = id;
            class_dto.number = Class.Number;
            class_dto.Level.Id = Class.Level.Id;
            class_dto.Level.Name = Class.Level.Name;
            class_dto.Department.Id = Class.Department.Id;
            class_dto.Department.Name = Class.Department.Name;
            class_dto.Term.Name = term.Name;
            class_dto.Term.Id = term.Id;

            foreach (var techerSub in Class.TeacherSubjectClasses)
            {
                TeacherSubjectDto teacherSubjectDto = new TeacherSubjectDto();
                teacherSubjectDto.Subject.Name = techerSub.Subject.Name;
                teacherSubjectDto.Subject.Id = techerSub.Subject.Id;
                if (techerSub.Teacher != null)
                {
                    teacherSubjectDto.Teacher.Name = techerSub.Teacher.Name;
                    teacherSubjectDto.Teacher.Id = techerSub.Teacher.Id;
                }


                class_dto.TeachersWithSubject.Add(teacherSubjectDto);

            }
            return class_dto;
        }*/
        public async Task <IEnumerable<ClassDtoWithId>> GetClassesByClassNum(int classnum)
        {
            var Classes = await _classRepository.GetClassesbyClassNum(classnum);
            Term term = await schoolRepository.GetCurrentTerm();
            if (Classes == null)
                return null;
            List<ClassDtoWithId> classes = new List<ClassDtoWithId>();
            foreach (var Class in Classes)
            {
                ClassWithTeacher_Subject class_dto = new ClassWithTeacher_Subject();

                class_dto.Id = Class.Id;
                class_dto.number = Class.Number;
                class_dto.Level.Id = Class.Level.Id;
                class_dto.Level.Name = Class.Level.Name;
                class_dto.Department.Id = Class.Department.Id;
                class_dto.Department.Name = Class.Department.Name;
                class_dto.Term.Name = term.Name;
                class_dto.Term.Id = term.Id;
                classes.Add(class_dto);
            }
            return classes;
        }

        
        public async Task<Class> AddClass(ClassAddUpdateDto classDto)
        {
            Class c =await _classRepository.CheckClassNumInLevel(classDto.Number, classDto.LevelId);
            if (c != null)
                return null;
            var classItem = _mapper.Map<Class>(classDto);

            await _unitOfWork.repository<Class>().Add(classItem);

            Term term = await schoolRepository.GetCurrentTerm();
            IEnumerable<Subject>Subjects = _subjectRecordRepository.GetSubjectsByLevelDeptTerm(classItem.LevelId, classItem.DepartmentId, term.Id).ToList();
            List< ClassTeacherSubjectDto >teacherSubjectClasses = new List< ClassTeacherSubjectDto >();
            foreach(var subject in Subjects)
            {
                teacherSubjectClasses.Add(new ClassTeacherSubjectDto() { SubjectId = subject.Id, ClassId = classItem.Id });
            }
            classItem.TeacherSubjectClasses = teacherSubjectClasses;
            await _classRepository.Update(classItem);
            return classItem;
        }

        public async Task<Class> UpdateClass(int id, ClassAddUpdateDto classDto)
        {
            var existingClass = await _classRepository.GetById(id);
            if (existingClass == null)
            {
                return null;
            }

            _mapper.Map(classDto, existingClass);
            await _classRepository.Update(existingClass);
            return existingClass;
        }

        public async Task<Class> DeleteClass(int id)
        {
            
            return await _classRepository.Delete(id);
            
        }

        public async Task<IEnumerable<TeachersSubjectDto>> GetSubjectWithThierTeachers(int classId)
        {
            List<TeachersSubjectDto> TeachersSubjectDto = new List<TeachersSubjectDto>();
            var _class = await _classRepository.GetClassWithTeacherAndSubject(classId);
            Term term = await schoolRepository.GetCurrentTerm();
            int TermId = term.Id;


            var subjectsWithTeachers = _subjectRecordRepository.GetSubjectsWithTeachersByLevelDeptTerm(_class.Level.Id, _class.Department.Id, TermId);
            if (subjectsWithTeachers != null)
            {
                foreach (Subject Subject in subjectsWithTeachers)
                {
                    TeachersSubjectDto teachersSubjectDto = new TeachersSubjectDto();
                    teachersSubjectDto.Subject.Name = Subject.Name;
                    teachersSubjectDto.Subject.Id = Subject.Id;
                    ClassTeacherSubjectDto tsc = _class.TeacherSubjectClasses.FirstOrDefault(tsc => tsc.SubjectId == Subject.Id);
                    if (tsc != null)
                    {
                        Teacher t = tsc.Teacher;
                        if (t != null)
                        {
                            teachersSubjectDto.ChosenTeacher.Name = t.Name;
                            teachersSubjectDto.ChosenTeacher.Id = t.Id;
                        }

                    }
                    foreach (var teacherSubject in Subject.TeachersSubject)
                    {

                        teachersSubjectDto.Teachers.Add(new NameIdDto()
                        {
                            Name = teacherSubject.Teacher.Name,
                            Id = teacherSubject.Teacher.Id

                        });
                    }
                    TeachersSubjectDto.Add(teachersSubjectDto);
                }
                return TeachersSubjectDto;
            }
            return null;
        }
        public async Task UpdateRecords(int classid , List<TeacherWithSubjectInClass> dto)
        {
            var _class = await _classRepository.GetClassWithTeacherSubjectClassById(classid);
            foreach (var teacherSubject in dto)
            {
                ClassTeacherSubjectDto tsc = _class.TeacherSubjectClasses.FirstOrDefault(cr => cr.SubjectId == teacherSubject.SubjectId);
                if(tsc != null) /////////////////####
                     tsc.TeacherId = teacherSubject.TeacherId;


            }
           await _classRepository.Update(_class);

        }
        public async Task<IEnumerable<ClassInfoDto>> GetTeacherClasses(int TeacherId)
        {
            List<ClassInfoDto>dtoLis = new List<ClassInfoDto>();
            IEnumerable<Class> classes = await _classRepository.GetTeacherClasses(TeacherId);
            foreach(Class _class in classes)
            {
                ClassInfoDto classInfoDto = new ClassInfoDto();
                classInfoDto.ClassId = _class.Id;
                classInfoDto.ClassLevelNumber = _class.Level.LevelNumber;
                classInfoDto.ClassNumber = _class.Number;
                classInfoDto.ClassDepartmentName = _class.Department.Name; 
                dtoLis.Add(classInfoDto);
            }
            return dtoLis;
        }


    }
}
