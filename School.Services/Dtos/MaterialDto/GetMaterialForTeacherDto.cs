using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.MaterialDto
{
    public class GetMaterialForTeacherDto
    {
        public List<NumIdDto> TeacherClasses { get; set; } = new List<NumIdDto>();
        public List<MaterialWithClasses> materialWithClasses { get; set; } = new List<MaterialWithClasses> ();

    }
}
