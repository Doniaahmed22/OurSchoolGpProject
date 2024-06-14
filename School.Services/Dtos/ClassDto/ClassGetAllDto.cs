using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class ClassGetAllDto
    {
        public List<ClassDtoWithId> Classes { get; set; } = new List<ClassDtoWithId>();
        public List<NameIdDto> Terms { get; set; } = new List<NameIdDto>();
        public List<NameIdDto> Levels { get; set; } = new List<NameIdDto>();
        public List<NameIdDto> Departments { get; set; } = new List<NameIdDto>();
    }
}
