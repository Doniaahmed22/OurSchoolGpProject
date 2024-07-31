using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.GradesDto;
using School.Services.Dtos.LevelDeoartmentDto;
using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.GradeService
{
    public class GradeServices : IGradeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubjectRecordRepository subjectRecordRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IGenericRepository<SchoolInfo> SchoolInfo;
        private readonly IGradeRepository gradeRepository;
        private readonly ISchoolRepository _schoolRepository;

        public GradeServices(IUnitOfWork unitOfWork, IMapper mapper, ISchoolRepository schoolRepository)
        {
            _unitOfWork = unitOfWork;
            subjectRecordRepository = (SubjectRecordRepository)_unitOfWork.repository<SubjectLevelDepartmentTerm>();
            SchoolInfo = _unitOfWork.repository<SchoolInfo>();
            gradeRepository = (GradeRepository)_unitOfWork.repository<StudentSubject>();
            studentRepository = (StudentRepository)_unitOfWork.repository<Student>();
            _schoolRepository = schoolRepository;
        }

        public async Task<List<StudentGradeDto>> GetStudentsWithGradesInSubjectbyClassId(int classid, int subjectid, string name = null)
        {
            List<StudentGradeDto> studentGradesDto = new List<StudentGradeDto>();
            IEnumerable<StudentSubject> studentSubjects;
            if (name == null)
                studentSubjects = await gradeRepository.GetStudentsWithGradesInSubjectbyClassId(classid, subjectid);
            else
                studentSubjects = await gradeRepository.GetStudentsGradesByName(classid, subjectid, name);

            foreach (var ss in studentSubjects)
            {
                StudentGradeDto studentGrade = new StudentGradeDto();
                studentGrade.student.Name = ss.student.Name;
                studentGrade.student.Id = ss.student.Id;
                studentGrade.Grades.midterm = ss.MidTerm;
                studentGrade.Grades.WorkYear = ss.WorkYear;
                studentGradesDto.Add(studentGrade);
            }
            return studentGradesDto;
        }
        public async Task< List<string>> validateStudentDegreeInSubject(int subjectid, StudentGradesBeforFinal studentgrade)
        {
            List<string> SubjectError = new List<string>();
            var DegreesLimit = await _schoolRepository.GetWorkyearMidTermFinal();
            string subname = (await _unitOfWork.repository<Subject>().GetById(subjectid)).Name;

            if (studentgrade.midterm < 0 || studentgrade.midterm > DegreesLimit.midterm)
                SubjectError.Add($"Midterm Grade for {subname} must be greater than or equal 0 and less than or equal {DegreesLimit.midterm}");
            if (studentgrade.WorkYear < 0 || studentgrade.WorkYear > DegreesLimit.Workyear)
                SubjectError.Add($"WorkYear Grade for {subname} must be greater than or equal 0 and less than or equal {DegreesLimit.Workyear}");
            return SubjectError;
        }
        public async Task<StudentSubject> UpdateStudentDegreeInSubject(int studentid, int subjectid, StudentGradesBeforFinal studentgrade)
        {
            StudentSubject StuGrades = await gradeRepository.GetStuGradesBySubjectId(studentid, subjectid);
            if (StuGrades == null)
                return null;
            StuGrades.MidTerm = studentgrade.midterm;
            StuGrades.WorkYear = studentgrade.WorkYear;
            await gradeRepository.Update(StuGrades);
            return StuGrades;
        }
        /////////////////level department//////////////////////
        public async Task<LevelsDepartmentsDto> GetLevelsDepartments()
        {
            var Levels = await _unitOfWork.repository<Level>().GetAll();
            var Departments = await _unitOfWork.repository<Department>().GetAll();
            LevelsDepartmentsDto levelsDepartments = new LevelsDepartmentsDto();
            foreach (var level in Levels)
            {
                levelsDepartments.Levels.Add(new NameIdDto() {Name = level.Name , Id = level.Id});
            }
            foreach (var dept in Departments)
            {
                levelsDepartments.Departments.Add(new NameIdDto() { Name = dept.Name, Id = dept.Id });
            }
            return levelsDepartments;

        }
        public async Task<StudentsFinalDegresDto> GetStudentsFinalGrades(int levelid, int departmentid, string name = "")
        {
            IEnumerable<SchoolInfo> schools = await _unitOfWork.repository<SchoolInfo>().GetAll();
            int currentterm = schools.ToList()[0].CurrentTerm;
            var subjects = subjectRecordRepository.GetSubjectsByLevelDeptTerm(levelid, departmentid, currentterm).ToList();
            StudentsFinalDegresDto StudentsFinalDegres = new StudentsFinalDegresDto();
            foreach (Subject subject in subjects)
            {
                StudentsFinalDegres.Subjects.Add(new NameIdDto { Name = subject.Name, Id = subject.Id });
            }
            IEnumerable<Student> students;
            if (name == "")
                students = await studentRepository.GetStudentsFinalDegreeByLevelDepart(levelid, departmentid);
            else
                students = await studentRepository.GetStudentsFinalGradesByName(levelid, departmentid, name);
            foreach (Student student in students)
            {
                if (student.StudentSubjects.Count < subjects.Count) {
                    student.StudentSubjects = new List<StudentSubject>();
                    foreach (Subject subject in subjects)
                    {
                        student.StudentSubjects.Add(new StudentSubject() { StudentId = student.Id, SubjectId = subject.Id });
                    }
                   await _unitOfWork.CompleteAsync();
                }
                StudentFinalGrade studentFinalGrade = new StudentFinalGrade();
                studentFinalGrade.Student.Name = student.Name;
                studentFinalGrade.Student.Id = student.Id;
                foreach (StudentSubject studentSubject in student.StudentSubjects)
                {
                    SubjectFinalDegree subjectFinalDegree = new SubjectFinalDegree();
                    subjectFinalDegree.Subject.Name = studentSubject.subject.Name;
                    subjectFinalDegree.Subject.Id = studentSubject.subject.Id;
                    subjectFinalDegree.FinalDegree = studentSubject.Final;
                    studentFinalGrade.subjectsFinalDegree.Add(subjectFinalDegree);
                }
                StudentsFinalDegres.studentsFinalGrade.Add(studentFinalGrade);
            }
            return StudentsFinalDegres;
        }
        public async Task< Dictionary<int, string>> validateFinalGradeForSubjects(List<SubjectFinalId> SubjectFinalIds)
        {
            Dictionary<int, string> SubjectError = new Dictionary<int, string>();
            int finalDegree = (await _schoolRepository.GetWorkyearMidTermFinal()).finalDegree;
            foreach (var SubjectFinalId in SubjectFinalIds)
            {
                if (SubjectFinalId.final >= 0 && SubjectFinalId.final <= finalDegree)
                    continue;
              string subname = ( await _unitOfWork.repository<Subject>().GetById(SubjectFinalId.SubjectId)).Name;
              SubjectError[SubjectFinalId.SubjectId] = $"Final Degree for {subname} Must Be Greater Than or Equal Zero and Less Than or Equal {finalDegree}";
            }
            return SubjectError;
        }
        public async Task<Student>UpdateFinalDegreeStudentId(int StudentId, List <SubjectFinalId> SubjectFinalIds)
        {
            Student student= await studentRepository.GetStudentWithSubjectDegrees(StudentId);
            if (student == null || student.StudentSubjects == null)
                return null;
            List<StudentSubject> studentSubjects = new List<StudentSubject> ();
            foreach(SubjectFinalId subjectFinalIds in SubjectFinalIds)
            {
                StudentSubject studentSubject = new StudentSubject();
                studentSubject.SubjectId = subjectFinalIds.SubjectId;
                studentSubject.Final = subjectFinalIds.final;
                studentSubjects.Add(studentSubject);
            }
            student.StudentSubjects = studentSubjects;
            await studentRepository.Update(student);
            return student;
        }
        public async Task<List<SubjectGradesDto>> GetStudentGradesByStuId(int id)
        {
            IEnumerable< StudentSubject> studentSubjects = await gradeRepository.GetStudentGradesByStuId(id);
            List<SubjectGradesDto>subjectsGrades = new List<SubjectGradesDto>();
            foreach(StudentSubject studentSubject in studentSubjects)
            {
                SubjectGradesDto subjectGrades = new SubjectGradesDto();
                subjectGrades.Subject.Name = studentSubject.subject.Name;
                subjectGrades.Subject.Id = studentSubject.subject.Id;
                subjectGrades.midTerm = studentSubject.MidTerm;
                subjectGrades.FinalGrade = studentSubject.Final;
                subjectGrades.Workyear = studentSubject.WorkYear;
                subjectGrades.total = subjectGrades.midTerm + subjectGrades.FinalGrade+ subjectGrades.Workyear;

                subjectsGrades.Add(subjectGrades);

            }
            return subjectsGrades;
        }

    }
}
