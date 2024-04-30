using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.TeacherDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.TeacherServices
{
    public class TeacherServices : ITeacherServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public TeacherServices (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeacherDtoWithId>> GetAllTeacher()
        {
            var teachers= await _unitOfWork.repository<Teacher>().GetAll();
            return _mapper.Map<IEnumerable<TeacherDtoWithId>>(teachers);
        }

        public async Task<TeacherDtoWithId> GetTeacherById(int id)
        {
            
            var teacher = await _unitOfWork.repository<Teacher>().GetById(id);
            if (teacher == null)
                return null;

            return _mapper.Map<TeacherDtoWithId>(teacher);

        }
        public async Task AddTeacher(TeacherDto teacherDto)
        {
           var teacher=  _mapper.Map<Teacher>(teacherDto);
           await _unitOfWork.repository<Teacher>().Add(teacher);
        }

        public async Task<Teacher> UpdateTeacher(int id ,TeacherDto dto)
        {
           var teacher= await _unitOfWork.repository<Teacher>().GetById(id);
            if (teacher == null)
                return null;
            _mapper.Map(dto, teacher);
            await _unitOfWork.repository<Teacher>().Update(teacher);
            return teacher;
        }
        public async Task<Teacher> DeleteTeacher(int id)
        {
            var teacher = await _unitOfWork.repository<Teacher>().GetById(id);
            if (teacher == null)
                return null;
            await _unitOfWork.repository<Teacher>().Delete(id); //???can we make delete function in generic just delete not find id ? 
            return teacher;
        }


    }
}
