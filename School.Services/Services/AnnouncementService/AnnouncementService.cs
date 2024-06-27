using AutoMapper;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.AnnouncementDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.AnnouncementService
{
    public class AnnouncementService : IAnnouncementService
    {

        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;

        public AnnouncementService(IAnnouncementRepository announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAnnouncements>> GetAllAnnouncementsAsync()
        {
            var announcements = await _announcementRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAnnouncements>>(announcements);
        }

        public async Task<IEnumerable<GetAnnouncements>> GetStudentsAnnouncementsAsync(int id)
        {
            var announcements = await _announcementRepository.GetStudentsAsync(id);
            var announce = await _announcementRepository.GetAnnouncementWithoutClassAsync();

            foreach (var item in announce)
            {
                announcements.Add(item);
            }

            return _mapper.Map<IEnumerable<GetAnnouncements>>(announcements);
        }

        public async Task<IEnumerable<GetAnnouncements>> GetParentsAnnouncementsAsync()
        {
            var announcements = await _announcementRepository.GetParentsAsync();
            return _mapper.Map<IEnumerable<GetAnnouncements>>(announcements);
        }

        public async Task<IEnumerable<GetAnnouncements>> GetTeachersAnnouncementsAsync()
        {
            var announcements = await _announcementRepository.GetTeachersAsync();
            return _mapper.Map<IEnumerable<GetAnnouncements>>(announcements);
        }

        public async Task<CreateSchoolDto> CreateSchoolAnnouncementAsync(CreateSchoolDto createSchoolDto)
        {
            var announcement = _mapper.Map<Announcement>(createSchoolDto);
            await _announcementRepository.AddAsync(announcement);
            await _announcementRepository.SaveChangesAsync();
            return _mapper.Map<CreateSchoolDto>(announcement);
        }

        public async Task<Announcement> CreateAnnouncementAsync(CreateAnnouncementDto dto)
        {
            var announcement = new Announcement
            {
                Title = dto.Title,
                Message = dto.Message,
                Subjects = dto.Subjects,
                AnnouncementClasses = dto.ClassIds.Select(id => new AnnouncementClass { ClassId = id }).ToList()
            };

            return await _announcementRepository.AddAnnouncementAsync(announcement);
        }

        public async Task<IEnumerable<Subject>> GetTeacherSubjectsAsync(int teacherId)
        {
            var teacherSubjects = await _announcementRepository.GetTeacherSubjectsAsync(teacherId);
            return teacherSubjects;
        }


        public async Task<IEnumerable<Class>> GetTeacherClassesBasedOnSubjectAsync(int teacherId, int subjectId)
        {
            var ClassBasedOnSubject = await _announcementRepository.GetTeacherClassesBasedOnSubjectAsync(teacherId, subjectId);
            return ClassBasedOnSubject;
        }

    }
}
