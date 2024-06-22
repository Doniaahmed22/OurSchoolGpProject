using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        IEnumerable<Subject> GetSubjectsOfTeacher(int TeachId);
        Task<IEnumerable<Subject>> GetSubjectsByClassTeacher(int classid, int teacherid);
        Task<IEnumerable<Subject>> GetSubjectsByStudId(int StuDepartmentId, int StuLevelId, int TermId);

    }
}
