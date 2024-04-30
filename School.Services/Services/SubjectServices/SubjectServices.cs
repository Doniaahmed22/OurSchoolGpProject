using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.SubjectDto;
using School.Services.Dtos.TeacherDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.SubjectServices
{
    public class SubjectServices:ISubjectServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectServices(IUnitOfWork unitOfWork, ISubjectRepository _subjectRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this._subjectRepository = _subjectRepository;
            _mapper = mapper;
        }

        public IEnumerable<BaseSubjectInfoDto> GetAllSubject()
        {
            
            ISubjectRepository SubjectRepository = (ISubjectRepository)_unitOfWork.repository<Subject>();
            IEnumerable<Subject> subjects = SubjectRepository.GetSubjectwithTermLevelDepartment();
            
            //IEnumerable<Subject> subjects = _subjectRepository.GetSubjectwithTermLevelDepartment();
            List <BaseSubjectInfoDto> SubjectsDto = new List<BaseSubjectInfoDto>();
            foreach (Subject subject in subjects)
            {
                BaseSubjectInfoDto SubjectDto =  new BaseSubjectInfoDto();
                SubjectDto.Id = subject.Id;
                SubjectDto.Name = subject.Name;
                foreach(SubjectTerm subjectTerm in subject.SubjectTerms)
                {
                    SubjectDto.SubjectTerms.Add(
                        new NameIdDto() {  Id = subjectTerm.Term.Id, Name = subjectTerm.Term.Name}
                    ); 
                }
                foreach (SubjectDepartment subjectDept in subject.SubjectDepartments)
                {
                    SubjectDto.SubjectDepartments.Add(
                        new NameIdDto() { Id = subjectDept.Department.Id , Name = subjectDept.Department.Name }
                    ); 
                }
                foreach (SubjectLevel subjectLevel in subject.SubjectLevels)
                {
                    SubjectDto.SubjectLevels.Add(
                        new NameIdDto() { Id = subjectLevel.Level.Id , Name = subjectLevel.Level.Name }
                    ); 
                }
                SubjectsDto.Add( SubjectDto );

            }
            return SubjectsDto;
        }



        public async Task<BaseSubjectInfoDto> GetSubjectById(int id)
        {

            ISubjectRepository SubjectRepository = (ISubjectRepository)_unitOfWork.repository<Subject>();
            var subject = await SubjectRepository.GetSubjectwithTermLevelDeptById(id);

            if (subject == null)
                return null;


            BaseSubjectInfoDto SubjectDto = new BaseSubjectInfoDto();
            SubjectDto.Id = subject.Id;
            SubjectDto.Name = subject.Name;
            foreach (SubjectTerm subjectTerm in subject.SubjectTerms)
            {
                SubjectDto.SubjectTerms.Add(
                new NameIdDto() { Id = subjectTerm.Term.Id, Name = subjectTerm.Term.Name }
                );
            }
            foreach (SubjectDepartment subjectDept in subject.SubjectDepartments)
            {
                SubjectDto.SubjectDepartments.Add(
                new NameIdDto() { Id = subjectDept.Department.Id, Name = subjectDept.Department.Name }
                );
            }
            foreach (SubjectLevel subjectLevel in subject.SubjectLevels)
            {
               SubjectDto.SubjectLevels.Add(
               new NameIdDto() { Id = subjectLevel.Level.Id, Name = subjectLevel.Level.Name }
              );
            }
            return SubjectDto;
        }

        public async Task AddSubject(SubjectDtoAddUpdate SubjectDto)
        {
            Subject subject = new Subject();
            subject.Name = SubjectDto.Name;
            subject.SubjectDepartments = new List<SubjectDepartment>();
            subject.SubjectDepartments.Add(new SubjectDepartment { DepartmentId = SubjectDto.DepatmentId });
            subject.SubjectTerms = new List<SubjectTerm>();
            subject.SubjectTerms.Add(new SubjectTerm { TermId = SubjectDto.TermId });
            subject.SubjectLevels = new List<SubjectLevel>();
            subject.SubjectLevels.Add(new SubjectLevel { LevelId = SubjectDto.LevelId });

            await _unitOfWork.repository<Subject>().Add(subject);
        }

        public async Task<Subject> UpdateSubject(int id, SubjectDtoAddUpdate dto)
        {
            throw new NotImplementedException();
        }
        public async Task<Subject> DeleteSubject(int id)
        {
            var subject = await _unitOfWork.repository<Subject>().GetById(id);
            if (subject == null)
                return null;
            await _unitOfWork.repository<Subject>().Delete(id); //???can we make delete function in generic just delete not find id ? 
            return subject;
        }

    }
}
