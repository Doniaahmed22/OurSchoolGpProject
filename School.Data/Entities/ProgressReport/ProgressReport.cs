using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities.ProgressReport
{
    public class ProgressReport:BaseEntity
    {

        [Required]
        public StatusType Status { get; set; } 

        [Required]
        [Range(0, 100)]
        public int ProgressLevel { get; set; }

        [Required]
        public AttitudeType Attitude { get; set; }
        public string Advantages { get; set; }

        public string Disadvantages { get; set; }

        public string Recommendations { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Today;
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
