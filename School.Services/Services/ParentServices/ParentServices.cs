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

        public async Task<IEnumerable<ParentDtoWithId>> GetAllParents()
        {
            var parents = await _unitOfWork.repository<Parent>().GetAll();
            return _mapper.Map<IEnumerable<ParentDtoWithId>>(parents);
        }

        public async Task<ParentDtoWithId> GetParentById(int id)
        {
            var parent = await _unitOfWork.repository<Parent>().GetById(id);
            return _mapper.Map<ParentDtoWithId>(parent);
        }

        public async Task AddParent(ParentDto parentDto)
        {
            var student = _mapper.Map<Parent>(parentDto);
            await _unitOfWork.repository<Parent>().Add(student);
        }

        public async Task UpdateParent(int id,ParentDto parentDto)
        {
            var existingParent = await _unitOfWork.repository<Parent>().GetById(id);
            if (existingParent == null)
            {
                throw new InvalidOperationException("Student not found");
            }

            _mapper.Map(parentDto, existingParent);
            await _unitOfWork.repository<Parent>().Update( existingParent);
        }

        public async Task DeleteParent(int id)
        {
            await _unitOfWork.repository<Parent>().Delete(id);
        }
    }
}
