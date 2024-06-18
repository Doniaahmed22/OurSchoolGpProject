using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.MaterialDto
{
    public class MaterialFieldDto
    {
        public string Name {  get; set; }
        public MaterialType MaterialType { get; set; }
    }
}
