using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.AnnouncementDto
{
    public class CreateSchoolDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int? ForWhich { get; set; }
    }
}
