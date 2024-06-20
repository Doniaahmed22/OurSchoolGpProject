using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class TeacherSubjectClass:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
