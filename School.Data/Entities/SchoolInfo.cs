using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class SchoolInfo
    {
        [Key]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Info { get; set; }
        public string Rules { get; set; }
    }
}
