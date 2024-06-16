using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class ClassMaterial:BaseEntity
    {
        public Material Material { get; set; }
        public int MaterialId { get; set; }
        public Class Class { get; set; }
        public int ClassId { get; set; }

    }
}
