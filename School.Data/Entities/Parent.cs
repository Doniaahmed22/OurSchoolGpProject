﻿using School.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Parent : BaseEntity
    {

        public int Id { get; set; }
       public string UserId { get; set; }
       //public AppUser User {  get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string GmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
