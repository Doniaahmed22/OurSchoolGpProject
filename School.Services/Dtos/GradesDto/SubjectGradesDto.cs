using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.GradesDto
{
    public class SubjectGradesDto
    {
        public NameIdDto Subject { get; set; } = new NameIdDto();
        public int FinalGrade {  get; set; }
        public int midTerm {  get; set; }
        public int Workyear {  get; set; }
        public int total {  get; set; }
    }
}
