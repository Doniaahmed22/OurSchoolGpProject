using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.ParentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.SubjectServices
{
    public class SubjectServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllSubject()
        {
            var subjects = await _unitOfWork.repository<Subject>().GetAll();
            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }

        public async Task<SubjectDto> GetSubjectById(int id)
        {
            var subject = await _unitOfWork.repository<Subject>().GetById(id);
            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task AddSubject(SubjectDto SubDto)
        {
            var subject = _mapper.Map<Subject>(SubDto);
            await _unitOfWork.repository<Subject>().Add(subject);
        }

        public async Task UpdateParent(int id, SubjectDto SubDto)
        {
            var existingSubject = await _unitOfWork.repository<Subject>().GetById(id);
            if (existingSubject == null)
            {
                throw new InvalidOperationException("Student not found");
            }

            _mapper.Map(SubDto, existingSubject);
            await _unitOfWork.repository<Subject>().Update(existingSubject);
        }

        public async Task DeleteParent(int id)
        {
            await _unitOfWork.repository<Subject>().Delete(id);
        }
    }
}
