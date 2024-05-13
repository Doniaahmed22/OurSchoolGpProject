using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        IEnumerable<Class> GetAllClasses();
        Task<Class> GetClassById(int id);
        Task<IEnumerable<TeacherSubjectClass>> GetClassRecordsByClassId(int classid);

        Task<Class> ClassDetaialsTeacherWithSubject(int id);

    }
}
