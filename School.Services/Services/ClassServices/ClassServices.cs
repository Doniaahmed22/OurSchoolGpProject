using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.ClassDto;
using School.Services.Dtos.SubjectRecord;

namespace School.Services.Services.ClassServices
{
    public class ClassServices : IClassServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public ClassServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _classRepository = (ClassRepository)_unitOfWork.repository<Class>();
        }

        public IEnumerable<ClassDtoWithId> GetAllClasses()
        {
            var classes = _classRepository.GetAllClasses();
            List<ClassDtoWithId> classesDto = new List<ClassDtoWithId>();
            foreach (var c in classes)
            {
                ClassDtoWithId classDto = new ClassDtoWithId();
                classDto.Id = c.Id;
                classDto.number = c.Number;
                classDto.NumOfStudent = c.NumOfStudent;
                classDto.Level.Id = c.LevelId;
                classDto.Level.Name = c.Level.Name;
                classDto.Department.Id = c.DepartmentId;
                classDto.Department.Name = c.Department.Name;
                classesDto.Add(classDto);
            }
            return classesDto;
        }

        public async Task<ClassDtoWithId> GetClassById(int id)
        {
            var c = await _classRepository.GetClassById(id);
            if (c == null)
                return null;
            ClassDtoWithId classDto = new ClassDtoWithId();
            classDto.Id = c.Id;
            classDto.number = c.Number;
            classDto.NumOfStudent = c.NumOfStudent;
            classDto.Level.Id = c.LevelId;
            classDto.Level.Name = c.Level.Name;
            classDto.Department.Id = c.DepartmentId;
            classDto.Department.Name = c.Department.Name;
            return classDto;
         }

        public async Task AddClass(ClassAddUpdateDto classDto)
        {
            var classItem = _mapper.Map<Class>(classDto);
            await _unitOfWork.repository<Class>().Add(classItem);
        }

        public async Task<Class> UpdateClass(int id, ClassAddUpdateDto classDto)
        {
            var existingClass = await _unitOfWork.repository<Class>().GetById(id);
            if (existingClass == null)
            {
                return null;
            }

            _mapper.Map(classDto, existingClass);
            await _unitOfWork.repository<Class>().Update(existingClass);
            return existingClass;
        }

        public async Task<Class> DeleteClass(int id)
        {
            return await _unitOfWork.repository<Class>().Delete(id);
            
        }
    }
}
