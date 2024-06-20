using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.LevelService
{
    public class LevelService:ILevelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LevelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NameIdDto>> GetAllLevelsForList()
        {
            List<NameIdDto> LevelsDto = new List<NameIdDto>();
            var Levels = await _unitOfWork.repository<Level>().GetAll();
            foreach (var level in Levels)
            {
                LevelsDto.Add(new NameIdDto() { Id = level.Id, Name = level.Name });
            }
            return LevelsDto;
        }

    }
}
