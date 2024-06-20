using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.SubjectDto
{
    public class SubLevelImage
    {
        public NameIdDto Subject {  get; set; } = new NameIdDto();
        public NumIdDto Level { get; set; } = new NumIdDto();
        public string image {  get; set; } 

    }
}
