using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class AbsenceWarning:BaseEntity
    {
        public int StudentId { get; set; }
        public DateTime WarningDate{ get; set; }
    }
}
