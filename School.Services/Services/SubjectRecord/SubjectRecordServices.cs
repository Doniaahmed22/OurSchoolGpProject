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

        private readonly IMapper _mapper;

        public SubjectRecordServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _subjectRecordRepository = (SubjectRecordRepository)_unitOfWork.repository<SubjectLevelDepartmentTerm>();
        }

        public  async Task<SubjectRecordGetAll> GetAllRecords()
        {
            SubjectRecordGetAll subjectRecordDto = new SubjectRecordGetAll();
            IEnumerable<Level> Levels = await _unitOfWork.repository<Level>().GetAll();
            foreach (Level level in Levels)
            {
                subjectRecordDto.Levels.Add(new NameIdDto() { Name = level.Name, Id = level.Id });
            }

            IEnumerable<Term> terms =await _unitOfWork.repository<Term>().GetAll();
            foreach (Term term in terms)
            {
                subjectRecordDto.Terms.Add(new NameIdDto() { Name =term.Name ,Id = term.Id });
            }
            IEnumerable<Department> Departments = await _unitOfWork.repository<Department>().GetAll();
            foreach (Department department in Departments)
            {
                subjectRecordDto.Departments.Add(new NameIdDto() { Name = department.Name, Id = department.Id });
            }

            IEnumerable<Subject> subjects = await _unitOfWork.repository<Subject>().GetAll();
            foreach (Subject subject in subjects)
            {
                subjectRecordDto.Subjects.Add(new NameIdDto() { Name = subject.Name, Id = subject.Id });
            }
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
            subjectRecordDto.subjectRecords = recordsDto;
            return subjectRecordDto;           
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
            var Record = _mapper.Map<SubjectLevelDepartmentTerm>(dto);
            await _unitOfWork.repository<SubjectLevelDepartmentTerm>().Add(Record);

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
