using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class ClassInfoDto
    {
        public int ClassId { get; set; }
        public int ClassNumber { get; set; }
        public string ClassDepartmentName { get; set; }
        public int ClassLevelNumber { get; set; }
    }
}
