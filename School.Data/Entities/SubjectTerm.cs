﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class SubjectTerm
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int TermId { get; set; }
        public Term Term { get; set; }

    }
}
