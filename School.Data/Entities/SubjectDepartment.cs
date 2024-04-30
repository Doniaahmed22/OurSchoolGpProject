using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class SubjectDepartment
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
