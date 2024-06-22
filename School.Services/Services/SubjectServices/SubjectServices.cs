using AutoMapper;
using Microsoft.AspNetCore.Http;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Dtos.SharedDto;
using School.Services.Dtos.SubjectDto;
using School.Services.Dtos.TeacherDto;
using School.Services.Services.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.SubjectServices
{
    public class SubjectServices:ISubjectServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IFileService _fileService;
        string[] allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png" };


        public SubjectServices(IUnitOfWork unitOfWork, IMapper mapper, ISubjectRepository subjectRepository 
            , IFileService fileService, IStudentRepository studentRepository, ISchoolRepository schoolRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _fileService = fileService;
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
        }

        public async Task<IEnumerable<SubjectDtoWithId>> GetAllSubject()
        {
            var subjects = await _unitOfWork.repository<Subject>().GetAll();
            return _mapper.Map<IEnumerable<SubjectDtoWithId>>(subjects);
        }

        public async Task<SubjectDtoWithId> GetSubjectById(int id)
        {
            var subject = await _unitOfWork.repository<Subject>().GetById(id);
            return _mapper.Map<SubjectDtoWithId>(subject);
        }
        public async Task<IEnumerable<SubIdNameImg>> GetSubjectsByStudId(int StudId)
        {
            List<SubIdNameImg> subjectsDtos = new List<SubIdNameImg>();
            var student = await _studentRepository.GetById(StudId);
            Term term = await _schoolRepository.GetCurrentTerm();
            var subjects = await _subjectRepository.GetSubjectsByStudId(student.DepartmentId.Value, student.LevelId.Value, term.Id);
            foreach (var subject in subjects) {
                subjectsDtos.Add(new SubIdNameImg() 
                    { image = _fileService.GetMediaUrl(GlobalStaticService.BaseImageSubject + subject.Image) , SubjectId = subject.Id , SubjectName = subject.Name}
                );
            }
            return subjectsDtos;
        }


        public async Task AddSubject(IFormFile image, SubjectDto SubDto)
        {
            string imagename;
            var subject = new Subject()
            {
                Name = SubDto.Name
            };
            if (image != null) 
            {
               string fullpath=await _fileService.SaveFileAsync(image, allowedImageExtensions, GlobalStaticService.BaseImageSubjectroot_Add, true);
               imagename = Path.GetFileName(fullpath);
               subject.Image = imagename;

            }


            await _unitOfWork.repository<Subject>().Add(subject);
        }
       // GetSubjectWithImage
        public async Task<Subject> UpdateSubject(int id, SubjectDto SubDto)
        {
            var subject = await _unitOfWork.repository<Subject>().GetById(id);
            if (subject == null)
                return null;
            _mapper.Map(SubDto, subject);
            await _unitOfWork.repository<Subject>().Update(subject);
            return subject;

        }

        public async Task<Subject> DeleteSubject(int id)
        {
            return await _unitOfWork.repository<Subject>().Delete(id);
        }
        public async Task<IEnumerable<NameIdDto>> GetSubjectsByClassTeacher(int classid, int teacherid)
        {
            List<NameIdDto> _Subjects = new List<NameIdDto>();
            IEnumerable<Subject> subjects = await _subjectRepository.GetSubjectsByClassTeacher(classid, teacherid);
            foreach (var subject in subjects)
            {
                NameIdDto sub = new NameIdDto();
                sub.Name = subject.Name;
                sub.Id = subject.Id;
                _Subjects.Add(sub);
            }
            return _Subjects;

        }
    }
}
