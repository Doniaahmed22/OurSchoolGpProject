using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.ClassRecord;
using School.Services.Dtos.SubjectRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ClassServices
{
    public class ClassRecordServices : IClassRecordServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TeacherSubjectClass> Repository;

        public ClassRecordServices (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Repository = _unitOfWork.repository<TeacherSubjectClass>();
        }
/*
        public IEnumerable<SubjectRecordDto> GetAllRecords()
        {
            var records = _subjectRecordRepository.GetAllRecord();
            List<SubjectRecordDto> recordsDto = new List<SubjectRecordDto>();
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
        public async Task<SubjectRecordDto> GetRecordById(int id)
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
        }*/
        public async Task AddRecord(ClassRecordDto dto)
        {
            var Record = _mapper.Map<TeacherSubjectClass>(dto);
            await Repository.Add(Record);

        }

        public async Task<TeacherSubjectClass> UpdateRecord(int id, ClassRecordDto record)
        {
            var ExRecord = await Repository.GetById(id);
            if (ExRecord == null)
            {
                return null;
            }

            _mapper.Map(record, ExRecord);
            await Repository.Update(ExRecord);
            return ExRecord;
        }

        public async Task<TeacherSubjectClass> DeleteRecord(int id)
        {

            return await Repository.Delete(id);

        }

    }
}
