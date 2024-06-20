using Microsoft.EntityFrameworkCore;
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
        Task<Class> GetClassWithTeacherSubjectClassById(int id);
        Task<IEnumerable<Class>> GetClassesWithTeacherLevelSubjectAsync(int teacherid, int levelid, int subjectid);

        Task<IEnumerable<TeacherSubjectClass>> GetClassRecordsByClassId(int classid);
        Task<IEnumerable<Subject>> GetSubjectsByClassTeacher(int classid, int teacherid);
        Task<Class> GetClassWithTeacherAndSubject(int id);
        Task<IEnumerable<Class>> GetClassesbyClassNum(int classnum);

    }
}
