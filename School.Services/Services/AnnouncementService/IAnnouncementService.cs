using School.Data.Entities;
using School.Services.Dtos.AnnouncementDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.AnnouncementService
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<GetAnnouncements>> GetAllAnnouncementsAsync();
        Task<IEnumerable<GetAnnouncements>> GetStudentsAnnouncementsAsync(int id);
        Task<IEnumerable<GetAnnouncements>> GetParentsAnnouncementsAsync();
        Task<IEnumerable<GetAnnouncements>> GetTeachersAnnouncementsAsync();
        Task<CreateSchoolDto> CreateSchoolAnnouncementAsync(CreateSchoolDto createSchoolDto);
        Task<Announcement> CreateAnnouncementAsync(CreateAnnouncementDto dto);
        Task<IEnumerable<Subject>> GetTeacherSubjectsAsync(int teacherId);
        Task<IEnumerable<Class>> GetTeacherClassesBasedOnSubjectAsync(int teacherId, int subjectId);


    }
}
