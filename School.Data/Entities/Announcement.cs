using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int? ForWhich { get; set; } = 1;
        public List<AnnouncementClass> AnnouncementClasses { get; set; } = new List<AnnouncementClass>();
        public String? Subjects { get; set; }
    }
}
