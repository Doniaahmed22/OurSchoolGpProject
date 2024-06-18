using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.MaterialDto
{
    public class MaterialWithClasses
    {
        public string MaterialName { get; set; }
        public int MaterialId { get; set; }
        public List<NumIdDto> MaterialToClasses { get; set; } = new List<NumIdDto>();
    }
}
