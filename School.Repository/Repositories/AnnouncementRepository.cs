using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;

namespace School.Repository.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {

        private readonly SchoolDbContext _context;

        public AnnouncementRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Announcement>> GetStudentsAsync(int studentId)
        {
            return  await (from a in _context.Announcements
                                 where a.ForWhich == 1 || a.ForWhich == 4
                                 join ac in _context.AnnouncementClasses on a.Id equals ac.AnnouncementId
                                 join s in _context.Students on ac.ClassId equals s.ClassId 
                                 where s.Id == studentId
                                 select a).Distinct().ToListAsync();
        }

        public async Task<List<Announcement>> GetAnnouncementWithoutClassAsync()
        {
            var announcements = await (from a in _context.Announcements
                                       where a.ForWhich == 1 || a.ForWhich == 4
                                       where !_context.AnnouncementClasses.Any(ac => ac.AnnouncementId == a.Id)
                                       select a).ToListAsync();

            return announcements;
        }

        public async Task<IEnumerable<Announcement>> GetParentsAsync()
        {
            return await _context.Announcements.Where(x => x.ForWhich == 2 || x.ForWhich == 4).ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> GetTeachersAsync()
        {
            return await _context.Announcements.Where(x => x.ForWhich == 3 || x.ForWhich == 4).ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task AddAsync(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Announcement> AddAnnouncementAsync(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<IEnumerable<Subject>> GetTeacherSubjectsAsync(int teacherId)
        {
                 return await _context.TeacherSubjects
                         .Where(ts => ts.TeacherId == teacherId)
                         .Select(ts => ts.Subject)
                         .ToListAsync();
        }


        public async Task<IEnumerable<Class>> GetTeacherClassesBasedOnSubjectAsync(int teacherId, int SubjectId)
        {
            return await _context.TeacherSubjectClasses
                    .Where(tsc => tsc.TeacherId == teacherId && tsc.SubjectId == SubjectId)
                    .Select(tsc => tsc.Class)
                    .ToListAsync();
        }


    }
}
