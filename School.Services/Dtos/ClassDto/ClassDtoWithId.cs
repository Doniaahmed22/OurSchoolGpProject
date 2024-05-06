using School.Data.Entities;
using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class ClassDtoWithId
    {
        public int Id { get; set; }
        public int number { get; set; }
        public int NumOfStudent { get; set; }
        public NameIdDto Department { get; set; } = new NameIdDto();
        public NameIdDto Level { get; set; } = new NameIdDto();

    }
}
