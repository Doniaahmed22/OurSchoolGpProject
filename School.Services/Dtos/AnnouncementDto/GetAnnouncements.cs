using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.AnnouncementDto
{
    public class GetAnnouncements
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
