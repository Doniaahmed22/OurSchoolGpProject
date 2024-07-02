using School.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities.ChatHub
{
    public class ActiveUserConnection:BaseEntity
    {
        public int Id { get; set; }  
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string ConnectionId { get; set; }
    }
}
