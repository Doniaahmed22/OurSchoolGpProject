using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ParentDto
{
    public class ParentDto
    {
        [Required]
        public string Name { get; set; }
        public string? Email { get; set; }
        [Required]
        public string GmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? UserId { get; set; }
    }
}
