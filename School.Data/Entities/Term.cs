using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Term:BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int termNumber { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

    }
}
