﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ParentDto
{
    public class ParentDto
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string GmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? UserId { get; set; }
    }
}
