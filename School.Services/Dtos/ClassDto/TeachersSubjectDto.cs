using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class TeachersSubjectDto
    {
        public List< NameIdDto> Teachers { get; set; } = new List<NameIdDto>();

        public NameIdDto Subject { get; set; } = new NameIdDto();

        public NameIdDto? ChosenTeacher { get; set; } = new NameIdDto();
    }
}
