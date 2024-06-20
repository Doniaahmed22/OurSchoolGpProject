using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.SharedDto;
using School.Services.Dtos.SubjectDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.DepartmentService
{
    public class DepartmentService : IdepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
   
        public async Task<IEnumerable<NameIdDto>> GetAllDepartmentForList()
        {
            List<NameIdDto>deptdto = new List<NameIdDto>();
            var departments = await _unitOfWork.repository<Department>().GetAll();
            foreach (var department in departments)
            {
                deptdto.Add(new NameIdDto() { Id = department.Id, Name = department.Name });
            }
            return deptdto;
        }


    }
}
