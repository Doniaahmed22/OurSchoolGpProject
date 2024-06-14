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
        private readonly IGenericRepository<TeacherSubjectClass> _classRecordRepoistory;
        private readonly ISchoolRepository schoolRepository;
        private readonly IMapper _mapper;

        public ClassServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _classRepository = (ClassRepository)_unitOfWork.repository<Class>();
            _subjectRecordRepository = (SubjectRecordRepository)_unitOfWork.repository<SubjectLevelDepartmentTerm>();
            _classRecordRepoistory = _unitOfWork.repository<TeacherSubjectClass>();
            schoolRepository = (SchoolRepository)_unitOfWork.repository<SchoolInfo>();

        }

        public async Task< ClassGetAllDto> GetAllClasses()
        {
            ClassGetAllDto classGetAllDto = new ClassGetAllDto();
            IEnumerable<Level> Levels = await _unitOfWork.repository<Level>().GetAll();
            foreach (Level level in Levels)
            {
                classGetAllDto.Levels.Add(new NameIdDto() { Name = level.Name, Id = level.Id });
            }

            IEnumerable<Term> terms = await _unitOfWork.repository<Term>().GetAll();
            foreach (Term term in terms)
            {
                classGetAllDto.Terms.Add(new NameIdDto() { Name = term.Name, Id = term.Id });
            }
            IEnumerable<Department> Departments = await _unitOfWork.repository<Department>().GetAll();
            foreach (Department department in Departments)
            {
                classGetAllDto.Departments.Add(new NameIdDto() { Name = department.Name, Id = department.Id });
            }

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
            classGetAllDto.Classes = classesDto;
            return classGetAllDto;
        }
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
        }
        public async Task <IEnumerable<ClassWithTeacher_Subject>> GetClassesByClassNum(int classnum)
        {
            var Classes = await _classRepository.GetClassesbyClassNum(classnum);
            Term term = await schoolRepository.GetCurrentTerm();
            if (Classes == null)
                return null;
            List<ClassWithTeacher_Subject> classes = new List<ClassWithTeacher_Subject>();
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
/*
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
*/
                classes.Add(class_dto);
            }
            return classes;

        }
        /////////////////////////////////////not important//////////////////////////////*
        /*
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
        /**/
        public async Task<Class> AddClass(ClassAddUpdateDto classDto)
        {
            var classItem = _mapper.Map<Class>(classDto);

            await _unitOfWork.repository<Class>().Add(classItem);

            Term term = await schoolRepository.GetCurrentTerm();
            IEnumerable<Subject>Subjects = _subjectRecordRepository.GetSubjectsByLevelDeptTerm(classItem.LevelId, classItem.DepartmentId, term.Id).ToList();
            List< TeacherSubjectClass >teacherSubjectClasses = new List< TeacherSubjectClass >();
            foreach(var subject in Subjects)
            {
                teacherSubjectClasses.Add(new TeacherSubjectClass() { SubjectId = subject.Id, ClassId = classItem.Id });
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

        public async Task< ClassAllTeachersWithSubjectDto> GetAssignTeachersInClass(int classId)
        {
            ClassAllTeachersWithSubjectDto TeachersWithSubjectDto;
            var _class = await _classRepository.GetClassWithTeacherAndSubject(classId);
            Term term = await schoolRepository.GetCurrentTerm();
            int TermId = term.Id;
            string TermName = term.Name;

            TeachersWithSubjectDto = new ClassAllTeachersWithSubjectDto
                (_class.Id, _class.Number, _class.NumOfStudent
                , new NameIdDto() { Id = _class.Department.Id, Name = _class.Department.Name },
                new NameIdDto() { Id = _class.Level.Id, Name = _class.Level.Name },
                new NameIdDto() { Id = TermId, Name = TermName }
                );
            var subjectsWithTeachers = _subjectRecordRepository.GetSubjectsWithTeachersByLevelDeptTerm(_class.Level.Id, _class.Department.Id, TermId);
            if (subjectsWithTeachers != null)
            {

                TeachersWithSubjectDto.PutTolist(subjectsWithTeachers, _class);
            }
     

            return TeachersWithSubjectDto;
        }
        public async Task UpdateRecords(int classid , List<TeacherSubjectUpdateClassRecordsDto> dto)
        {
            var _class = await _classRepository.GetClassWithTeacherSubjectClassById(classid);
            foreach (var teacherSubject in dto)
            {
                TeacherSubjectClass tsc = _class.TeacherSubjectClasses.FirstOrDefault(cr => cr.SubjectId == teacherSubject.SubjectId);
                tsc.TeacherId = teacherSubject.TeacherId;


            }
           await _classRepository.Update(_class);

        }


        public async Task<IEnumerable<NameIdDto>> GetSubjectsByClassTeacher(int classid, int teacherid)
        {
            List<NameIdDto>_Subjects = new List<NameIdDto>();
            IEnumerable<Subject>  subjects= await _classRepository.GetSubjectsByClassTeacher(classid, teacherid);
            foreach(var subject in subjects)
            {
                NameIdDto sub = new NameIdDto();
                sub.Name = subject.Name;
                sub.Id = subject.Id;
                _Subjects.Add(sub);
            }
            return _Subjects;

        }
    }
}
