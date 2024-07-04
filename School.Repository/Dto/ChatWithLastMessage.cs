using School.Data.Entities;
using School.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Dto
{
    public class ChatWithLastMessage
    {
        public AppUser User { get; set; }
        public Message LastMessage { get; set; }
    }
}
