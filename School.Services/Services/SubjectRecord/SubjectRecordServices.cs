using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.SharedDto;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.SubjectRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.SubjectRecord
{
    public class SubjectRecordServices:ISubjectRecordServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubjectRecordRepository _subjectRecordRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public SubjectRecordServices(IUnitOfWork unitOfWork, IStudentRepository studentRepository,ISchoolRepository schoolRepository, IMapper mapper, IClassRepository classRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _subjectRecordRepository = (SubjectRecordRepository)_unitOfWork.repository<SubjectLevelDepartmentTerm>();
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
        }

        public  async Task<IEnumerable<SubjectRecordDto>> GetAllRecords()
        {
            var records =  _subjectRecordRepository.GetAllRecord();
            List<SubjectRecordDto>recordsDto = new List<SubjectRecordDto>();
            foreach (var record in records)
            {
                SubjectRecordDto recordDto = new SubjectRecordDto();
                recordDto.SubLevlDeptTermId = record.Id;
                
                recordDto.Subject.Id = record.Subject.Id;
                recordDto.Subject.Name = record.Subject.Name;
                recordDto.Level.Id = record.LevelId;
                recordDto.Level.Name = record.Level.Name;
                recordDto.Department.Id = record.DepartmentId;
                recordDto.Department.Name = record.Department.Name;
                recordDto.Term.Id = record.TermId;
                recordDto.Term.Name = record.Term.Name;
                recordsDto.Add(recordDto);
            }

            return recordsDto;           
        }


        public async Task <SubjectRecordDto> GetRecordById(int id)
        {
            var record = await _subjectRecordRepository.GetRecordById(id);
            if (record == null)
                return null;
            SubjectRecordDto recordDto = new SubjectRecordDto();
            recordDto.SubLevlDeptTermId = record.Id;

            recordDto.Subject.Id = record.Subject.Id;
            recordDto.Subject.Name = record.Subject.Name;
            recordDto.Level.Id = record.LevelId;
            recordDto.Level.Name = record.Level.Name;
            recordDto.Department.Id = record.DepartmentId;
            recordDto.Department.Name = record.Department.Name;
            recordDto.Term.Id = record.TermId;
            recordDto.Term.Name = record.Term.Name;
            return recordDto;
        }
        public async Task<IEnumerable<SubjectRecordDto>> SearchBySubjectName(string name)
        {
            var records = await _subjectRecordRepository.GetRecordsBySubjectName(name);
            if (records == null)
                return null;
            List<SubjectRecordDto>dtos = new List<SubjectRecordDto>();
            foreach(var record in records)
            {
                SubjectRecordDto recordDto = new SubjectRecordDto();

                recordDto.SubLevlDeptTermId = record.Id;

                recordDto.Subject.Id = record.Subject.Id;
                recordDto.Subject.Name = record.Subject.Name;
                recordDto.Level.Id = record.LevelId;
                recordDto.Level.Name = record.Level.Name;
                recordDto.Department.Id = record.DepartmentId;
                recordDto.Department.Name = record.Department.Name;
                recordDto.Term.Id = record.TermId;
                recordDto.Term.Name = record.Term.Name;
                dtos.Add(recordDto);
            }
            return dtos;

        }

        public async Task AddRecord(Dtos.SubjectRecord.SubjectRecordAddUpdateDto dto)
        {
            Term term =await _schoolRepository.GetCurrentTerm();
            var Record = _mapper.Map<SubjectLevelDepartmentTerm>(dto);

             _unitOfWork.repository<SubjectLevelDepartmentTerm>().AddWithoutSave(Record);
            if (term.Id == Record.TermId)
            {
                IEnumerable<Student> students = await _studentRepository.GetStudentsByLevelIdDepartmentId(Record.LevelId, Record.DepartmentId);
                foreach (Student student in students)
                {
                    if (student.StudentSubjects == null)
                    {
                        student.StudentSubjects = new List<StudentSubject>();
                        IEnumerable<Subject> subjects = _subjectRecordRepository.GetSubjectsByLevelDeptTerm(Record.LevelId, Record.DepartmentId, term.Id);
                        foreach (var subject in subjects)
                            student.StudentSubjects.Add(new StudentSubject() { StudentId = student.Id, SubjectId = subject.Id });
                    }
                    student.StudentSubjects.Add(new StudentSubject() { StudentId = student.Id, SubjectId = Record.SubjectId });
                }
                IEnumerable<Class> classes = await _classRepository.GetClassesByLevelDepartment(Record.LevelId, Record.DepartmentId);
                foreach(Class _class in classes)
                {
                    _class.TeacherSubjectClasses.Add(new ClassTeacherSubjectDto()
                    {
                        ClassId = _class.Id,
                        SubjectId = Record.SubjectId
                    });
                }

            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task< SubjectLevelDepartmentTerm>UpdateRecord(int id,SubjectRecordAddUpdateDto record)
        {
            var ExRecord = await _unitOfWork.repository<SubjectLevelDepartmentTerm>().GetById(id);
            if (ExRecord == null)
            {
                return null;
            }

            _mapper.Map(record, ExRecord);
            await _unitOfWork.repository<SubjectLevelDepartmentTerm>().Update(ExRecord);
            return ExRecord;
        }

        public async Task<SubjectLevelDepartmentTerm> DeleteRecord(int id)
        {

           return await _unitOfWork.repository<SubjectLevelDepartmentTerm>().Delete(id);

        }


    }
}
