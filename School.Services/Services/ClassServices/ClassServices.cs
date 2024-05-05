using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
//using School.Services.Dtos.ClassDto;
/*
namespace School.Services.Services.ClassServices
{
    public class ClassServices : IClassServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassDtoWithId>> GetAllClasses()
        {
            var classes = await _unitOfWork.Repository<Class>().GetAll();
            return _mapper.Map<IEnumerable<ClassDtoWithId>>(classes);
        }

        public async Task<ClassDtoWithId> GetClassById(int id)
        {
            var classItem = await _unitOfWork.Repository<Class>().GetById(id);
            return _mapper.Map<ClassDtoWithId>(classItem);
        }

        public async Task AddClass(ClassDto classDto)
        {
            var classItem = _mapper.Map<Class>(classDto);
            await _unitOfWork.Repository<Class>().Add(classItem);
        }

        public async Task UpdateClass(int id, ClassDto classDto)
        {
            var existingClass = await _unitOfWork.Repository<Class>().GetById(id);
            if (existingClass == null)
            {
                throw new InvalidOperationException("Class not found");
            }

            _mapper.Map(classDto, existingClass);
            await _unitOfWork.Repository<Class>().Update(existingClass);
        }

        public async Task DeleteClass(int id)
        {
            await _unitOfWork.Repository<Class>().Delete(id);
        }
    }
}
*/