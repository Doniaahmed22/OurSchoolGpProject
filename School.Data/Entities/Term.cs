using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities
{
    public class Term
    {
        public int Id { get; set; }
        public List<SubjectTerm> SubjectTerms { get; set;}
    }
}
