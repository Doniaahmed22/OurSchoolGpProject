using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.MaterialDto
{
    public class MaterialAddDto
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int Levelid { get; set; }

        public List<int>MaterialClasses { get; set; } = new List<int>();

    }
}
