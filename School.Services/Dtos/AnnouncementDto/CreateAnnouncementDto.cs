using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.AnnouncementDto
{
    public class CreateAnnouncementDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Subjects { get; set; }
        public List<int> ClassIds { get; set; }

    }
}
