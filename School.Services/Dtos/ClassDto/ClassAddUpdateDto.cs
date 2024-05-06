using School.Data.Entities;
using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class ClassAddUpdateDto
    {

        public int Number { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }
        public int NumOfStudent { get; set; }

    }
}
