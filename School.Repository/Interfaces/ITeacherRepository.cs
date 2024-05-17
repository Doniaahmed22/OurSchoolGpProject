using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        IEnumerable<Teacher> GetTeachersWithSubject();
        Task<Teacher> GetTeachersWithSubjectById(int techerid);
        IEnumerable<Teacher> GetTeachersByName(string name);


    }
}
