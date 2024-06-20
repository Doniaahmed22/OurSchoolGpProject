using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Material:BaseEntity
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public MaterialType Type { get; set; }
        public Level Level { get; set; }
        public int Levelid { get; set; }

        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get;set; }
        public List<ClassMaterial> ClassMaterials { get; set; }

    }
}
