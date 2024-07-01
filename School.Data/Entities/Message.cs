using School.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Message :BaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime MessageDate { get; set; }
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }      
        public string ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        
    }
}
