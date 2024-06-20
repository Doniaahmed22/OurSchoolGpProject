using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class SchoolInfo:BaseEntity
    {
        [Key]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Info { get; set; }
        public string Rules { get; set; }

        [ForeignKey("Term")]
        public int CurrentTerm { get; set; } 
        public Term Term { get; set; } 
    }
}
