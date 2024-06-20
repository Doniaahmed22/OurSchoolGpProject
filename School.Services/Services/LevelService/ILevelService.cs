using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.LevelService
{
    public interface ILevelService
    {
        Task<IEnumerable<NameIdDto>> GetAllLevelsForList();
    }
}
