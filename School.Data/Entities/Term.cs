﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Term
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

    }
}
