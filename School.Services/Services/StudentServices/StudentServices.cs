using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.StudentDto;

namespace School.Services.Services.StudentServices
{
    public class StudentServices : IStudentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            var students = await _unitOfWork.repository<Student>().GetAll();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            var student = await _unitOfWork.repository<Student>().GetById(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task AddStudent(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            await _unitOfWork.repository<Student>().Add(student);
        }

        public async Task UpdateStudent(StudentDto studentDto)
        {
            var id = studentDto.Id;
            var existingStudent = await _unitOfWork.repository<Student>().GetById(id);
            if (existingStudent == null)
            {
                throw new InvalidOperationException("Student not found");
            }

            _mapper.Map(studentDto, existingStudent);
            await _unitOfWork.repository<Student>().Update(existingStudent);           
        }

        public async Task DeleteStudent(int id)
        {
            await _unitOfWork.repository<Student>().Delete(id);
        }
    }
}






        