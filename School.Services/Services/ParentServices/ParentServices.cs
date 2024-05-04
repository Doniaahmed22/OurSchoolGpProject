using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.ParentDto;

namespace School.Services.Services.ParentServices
{
    public class ParentServices : IParentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParentServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllParents()
        {
            var parents = await _unitOfWork.repository<Subject>().GetAll();
            return _mapper.Map<IEnumerable<SubjectDto>>(parents);
        }

        public async Task<SubjectDto> GetParentById(int id)
        {
            var parent = await _unitOfWork.repository<Subject>().GetById(id);
            return _mapper.Map<SubjectDto>(parent);
        }

        public async Task AddParent(SubjectDto parentDto)
        {
            var student = _mapper.Map<Subject>(parentDto);
            await _unitOfWork.repository<Subject>().Add(student);
        }

        public async Task UpdateParent(int id,SubjectDto parentDto)
        {
            var existingParent = await _unitOfWork.repository<Subject>().GetById(id);
            if (existingParent == null)
            {
                throw new InvalidOperationException("Student not found");
            }

            _mapper.Map(parentDto, existingParent);
            await _unitOfWork.repository<Subject>().Update(existingParent);
        }

        public async Task DeleteParent(int id)
        {
            await _unitOfWork.repository<Subject>().Delete(id);
        }
    }
}
