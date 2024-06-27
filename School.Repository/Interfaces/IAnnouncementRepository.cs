using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<IEnumerable<Announcement>> GetParentsAsync();
        Task<IEnumerable<Announcement>> GetTeachersAsync();
        Task<List<Announcement>> GetStudentsAsync(int studentId);
        Task<List<Announcement>> GetAnnouncementWithoutClassAsync();
        //Task<Announcement> GetByIdAsync(int id);
        Task AddAsync(Announcement announcement);
        Task SaveChangesAsync();
        Task<Announcement> AddAnnouncementAsync(Announcement announcement);
        Task<IEnumerable<Subject>> GetTeacherSubjectsAsync(int teacherId);
        Task<IEnumerable<Class>> GetTeacherClassesBasedOnSubjectAsync(int teacherId, int SubjectId);

    }
}
