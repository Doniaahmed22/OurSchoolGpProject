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
            IEnumerable<SubjectLevelDepartmentTerm> SubjectLevelDepartmentTerms = SubjectRepository.GetSubjectLevelDepartmentTerm();
            
            //IEnumerable<Subject> subjects = _subjectRepository.GetSubjectwithTermLevelDepartment();
            List <BaseSubjectInfoDto> SubjectsDto = new List<BaseSubjectInfoDto>();
            foreach (var SubLevelDeptTerm in SubjectLevelDepartmentTerms)
            {
                BaseSubjectInfoDto SubjectDto =  new BaseSubjectInfoDto();
                SubjectDto.SubLevlDeptTermId = SubLevelDeptTerm.Id;
                SubjectDto.Subject.Name = SubLevelDeptTerm.Subject.Name;
                SubjectDto.Subject.Id = SubLevelDeptTerm.SubjectId;

                SubjectDto.SubjectLevel.Name = SubLevelDeptTerm.Level.Name;
                SubjectDto.SubjectLevel.Id = SubLevelDeptTerm.LevelId;
                SubjectDto.SubjectDepartment.Name = SubLevelDeptTerm.Department.Name;
                SubjectDto.SubjectDepartment.Id = SubLevelDeptTerm.DepartmentId;
                SubjectDto.SubjectTerm.Name = SubLevelDeptTerm.Term.Name;
                SubjectDto.SubjectTerm.Id = SubLevelDeptTerm.TermId;

                SubjectsDto.Add( SubjectDto );

            }
            return SubjectsDto;
        }



        public async Task<BaseSubjectInfoDto> GetSubjectById(int id)
        {

            ISubjectRepository SubjectRepository = (ISubjectRepository)_unitOfWork.repository<Subject>();
            var SubLevelDeptTermId = await SubjectRepository.GetSubjectwithTermLevelDeptById(id);

            if (SubLevelDeptTermId == null)
                return null;


            BaseSubjectInfoDto SubjectDto = new BaseSubjectInfoDto();
            SubjectDto.SubLevlDeptTermId = SubLevelDeptTermId.Id;
            SubjectDto.Subject.Name = SubLevelDeptTermId.Subject.Name;
            SubjectDto.Subject.Id = SubLevelDeptTermId.Subject.Id;
            SubjectDto.SubjectLevel.Name = SubLevelDeptTermId.Level.Name;
            SubjectDto.SubjectLevel.Id = SubLevelDeptTermId.LevelId;
            SubjectDto.SubjectDepartment.Name = SubLevelDeptTermId.Department.Name;
            SubjectDto.SubjectDepartment.Id = SubLevelDeptTermId.DepartmentId;
            SubjectDto.SubjectTerm.Name = SubLevelDeptTermId.Term.Name;
            SubjectDto.SubjectTerm.Id = SubLevelDeptTermId.TermId;
            return SubjectDto;
        }

        public async Task AddSubject(SubjectDtoAddUpdate SubjectDto)
        {
            SubjectLevelDepartmentTerm SubLevelDeptTerm = new SubjectLevelDepartmentTerm();
            SubLevelDeptTerm.SubjectId = SubjectDto.SubjectId;
            SubLevelDeptTerm.LevelId = SubjectDto.LevelId;
            SubLevelDeptTerm.DepartmentId = SubjectDto.DepatmentId;
            SubLevelDeptTerm.TermId = SubjectDto.TermId;


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
