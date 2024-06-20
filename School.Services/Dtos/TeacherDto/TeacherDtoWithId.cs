using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.TeacherDto
{
    public class TeacherDtoWithId:TeacherDto
    {
        public int Id { get; set; }
    }
}
