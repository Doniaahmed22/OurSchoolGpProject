using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.MaterialDto
{
    public class MaterialAddDto2
    {
        public string SubjectId { get; set; }
        public string TeacherId { get; set; }
        public string Levelid { get; set; }
        public string MaterialClass { get; set; }
        public IFormFile material {  get; set; }
    }
}
