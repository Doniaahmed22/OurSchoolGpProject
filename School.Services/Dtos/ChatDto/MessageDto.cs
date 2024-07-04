using School.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ChatDto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime MessageDate { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

    }
}
