using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class AnnouncementClass
    {
        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
