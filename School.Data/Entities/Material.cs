using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public MaterialType Type { get; set; }
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get;set; }
        public List<ClassMaterial> ClassMaterials { get; set; }

    }
}
