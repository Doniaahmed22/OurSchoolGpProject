﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.MaterialDto;
using School.Services.Dtos.SharedDto;
using School.Services.Services.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.MaterialService
{
    public class MaterialService:IMaterialService
    {
        private readonly IFileService _fileService;
        private readonly IMaterialRepository _materialRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly string BaseSubFolder = "wwwroot/Uploads/Materials";
        string[] allowedVideoExtensions = { ".mp4", ".avi", ".mov", ".wmv" };
        string[] allowedSummaryExtensions = { ".txt", ".doc", ".docx", ".pdf" };
        string[] allowedBookExtensions = { ".pdf", ".epub", ".mobi" };
        string[] allowedExamExtensions = { ".docx", ".pdf"};
        string[] allowedRevisionExtensions = { ".txt", ".doc", ".docx", ".pdf", ".pptx", ".ppt"  };

        public MaterialService(IFileService fileService , IMaterialRepository materialRepository,
            IClassRepository classRepository, IUnitOfWork unitOfWork, IStudentRepository studentRepository)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _materialRepository = materialRepository;
            _classRepository = classRepository;
            _studentRepository = studentRepository;
        }
        public List<MaterialFieldDto> GetMaterialFields()
        {
            List<MaterialFieldDto> MaterialFields = new List<MaterialFieldDto>();
            MaterialFields.Add(new MaterialFieldDto() { Name = "Book", MaterialType = MaterialType.Book });
            MaterialFields.Add(new MaterialFieldDto() { Name = "Video", MaterialType = MaterialType.Video });
            MaterialFields.Add(new MaterialFieldDto() { Name = "Summary", MaterialType = MaterialType.Summary });
            MaterialFields.Add(new MaterialFieldDto() { Name = "Exam", MaterialType = MaterialType.Exam });
            MaterialFields.Add(new MaterialFieldDto() { Name = "Revision", MaterialType = MaterialType.Revision });

            return MaterialFields;
        }

        public async Task<IEnumerable<MaterialWithClasses>> GetMaterialsForTeacher(MaterialType MaterialType, int teacherid, int levelid ,int subjectid ,int? classid=null)
        {
            List<MaterialWithClasses> materialsWithClassesDto = new List<MaterialWithClasses>();
            List<Material> materials= await _materialRepository.GetMaterialWithClassesAsync(MaterialType,teacherid, levelid,subjectid, classid);

            foreach (Material material in materials)
            {
                MaterialWithClasses dto = new MaterialWithClasses();
                dto.MaterialId = material.Id;
                dto.MaterialName = material.MaterialName;
                foreach (var classmaterial in material.ClassMaterials)
                {
                    dto.ClassesTakeMaterial.Add(new NumIdDto()
                    { num = classmaterial.Class.Number, Id = classmaterial.Class.Id }
                    );
                }
                materialsWithClassesDto.Add(dto);
            }
            return materialsWithClassesDto;
        }

        public async Task<IEnumerable<NameIdDto>> GetMaterialForStudent(MaterialType MaterialType,int SubjectId,int StudentId)
        {
            List<NameIdDto> MaterialsDto = new List<NameIdDto>();
            var student = await _studentRepository.GetById(StudentId);
            
            if(student.ClassId==null)
                return null;
            IEnumerable<Material>materials= await _materialRepository.GetMaterialForStudent(MaterialType, SubjectId, student.ClassId.Value);
            foreach (Material material in materials)
            {
                MaterialsDto.Add(new NameIdDto() { Id = material.Id , Name = material.MaterialName });
            }
            return MaterialsDto;
        }
        public async Task<string> UploadMaterial(IFormFile File, MaterialType materialType, MaterialAddDto dto)
        {
            string[] allowedExtension = allowedRevisionExtensions;
            string subfolder = "";
            Material mat = await _materialRepository.GetMaterialOfNameAsync(File.FileName, materialType, Int32.Parse(dto.TeacherId), Int32.Parse(dto.Levelid), Int32.Parse(dto.SubjectId));
            if (mat != null)
                return null;
            if (materialType == MaterialType.Video)
            {
                allowedExtension = allowedVideoExtensions;
                subfolder = "/Video";

            }
            else if (materialType == MaterialType.Exam)
            {
                allowedExtension = allowedExamExtensions;
                subfolder = "/Exam";
            }
            else if (materialType == MaterialType.Summary)
            {
                allowedExtension = allowedSummaryExtensions;
                subfolder = "/Summary";
            }
            else if (materialType == MaterialType.Revision)
            {
                allowedExtension = allowedRevisionExtensions;
                subfolder = "/Revision";
            }
            else if (materialType == MaterialType.Book)
            {
                allowedExtension = allowedBookExtensions;
                subfolder = "/Book";
            }
            var filePath = await _fileService.SaveFileAsync(File, allowedExtension, BaseSubFolder + subfolder, false);
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            var fileName = Path.GetFileName(filePath);


            var uploadedMaterial = new Material
            {
                MaterialName = fileName,
                SubjectId =Int32.Parse(dto.SubjectId) ,
                TeacherId = Int32.Parse(dto.TeacherId),
                Levelid = Int32.Parse(dto.Levelid),
                Type = materialType

            };
            await _materialRepository.Add(uploadedMaterial);
            foreach (string _class in dto.MaterialClasses)
            {
                _unitOfWork.repository<ClassMaterial>()
                    .AddWithoutSave(new ClassMaterial
                    {
                        ClassId = Int32.Parse(_class),
                        MaterialId = uploadedMaterial.Id
                    });
            }
            await _unitOfWork.CompleteAsync();
            return fileName;
        }
        public async Task<string> UploadMaterial(IFormFile File, MaterialType materialType, MaterialAddDto2 dto)
        {
            string[] allowedExtension = allowedRevisionExtensions;
            string subfolder = "";
            Material mat = await _materialRepository.GetMaterialOfNameAsync(File.FileName, materialType, Int32.Parse( dto.TeacherId), Int32.Parse( dto.Levelid), Int32.Parse(dto.SubjectId));
            if (mat != null)
                return null;
            if (materialType == MaterialType.Video)
            {
                allowedExtension = allowedVideoExtensions;
                subfolder = "/Video";

            }
            else if (materialType == MaterialType.Exam)
            {
                allowedExtension = allowedExamExtensions;
                subfolder = "/Exam";
            }
            else if (materialType == MaterialType.Summary)
            {
                allowedExtension = allowedSummaryExtensions;
                subfolder = "/Summary";
            }
            else if (materialType == MaterialType.Revision)
            {
                allowedExtension = allowedRevisionExtensions;
                subfolder = "/Revision";
            }
            else if (materialType == MaterialType.Book)
            {
                allowedExtension = allowedBookExtensions;
                subfolder = "/Book";
            }
            var filePath = await _fileService.SaveFileAsync(File, allowedExtension, BaseSubFolder + subfolder, false);
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            var fileName = Path.GetFileName(filePath);


            var uploadedMaterial = new Material
            {
                MaterialName = fileName,
                SubjectId = Int32.Parse( dto.SubjectId),
                TeacherId = Int32.Parse(dto.TeacherId),
                Levelid = Int32.Parse(dto.Levelid),
                Type = materialType,

            };
            await _materialRepository.Add(uploadedMaterial);
            _unitOfWork.repository<ClassMaterial>()
                .AddWithoutSave(new ClassMaterial
                {
                    ClassId = Int32.Parse(dto.MaterialClass),
                    MaterialId = uploadedMaterial.Id
                });
            await _unitOfWork.CompleteAsync();
            return fileName;
        }


        public async Task<(MemoryStream stream, string contentType, string fileName)> DownloadMaterial( int MaterialId)

        {
            var Material = await _materialRepository.GetById(MaterialId);
            string fileName = Material.MaterialName;
            MaterialType materialType = Material.Type;
            var filePath = GetFilePath(fileName, materialType);
            return await _fileService.DownloadFileAsync(filePath);
        }
        public async Task DeleteMaterial( int MaterialId)
        {
            var Material = await _materialRepository.GetById(MaterialId);
            string fileName = Material.MaterialName;
            MaterialType materialType = Material.Type;
            var filePath = GetFilePath(fileName, materialType);
            _fileService.DeleteFile(filePath);
            await _materialRepository.Delete(MaterialId);
        }
        private string GetFilePath(string fileName, MaterialType materialType)
        {

            string subfolder = "";
            if (materialType == MaterialType.Video)
                subfolder = "/Video";
            else if (materialType == MaterialType.Exam)
                subfolder = "/Exam";
            else if (materialType == MaterialType.Summary)
                subfolder = "/Summary";
            else if (materialType == MaterialType.Revision)
                subfolder = "/Revision";
            else if (materialType == MaterialType.Book)
                subfolder = "/Book";
            return Path.Combine(BaseSubFolder + subfolder, fileName);
        }
    }
}
