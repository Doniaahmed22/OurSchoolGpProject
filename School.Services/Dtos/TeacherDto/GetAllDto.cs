using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.TeacherDto
{
    public class GetAllDto
    {
        public IEnumerable< TeacherDtoWithId>teachers { get; set; } = new List< TeacherDtoWithId>();
        public List<NameIdDto> Subjects { get; set; } = new List<NameIdDto>();
    }
}
