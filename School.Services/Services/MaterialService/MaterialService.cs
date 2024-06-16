using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.MaterialDto;
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
        private readonly IGenericRepository<Material> _materialRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly string BaseSubFolder = "wwwroot/Uploads/Materials";
        string[] allowedVideoExtensions = { ".mp4", ".avi", ".mov", ".wmv" };
        string[] allowedSummaryExtensions = { ".txt", ".doc", ".docx", ".pdf" };
        string[] allowedBookExtensions = { ".pdf", ".epub", ".mobi" };
        string[] allowedExamExtensions = { ".docx", ".pdf"};
        string[] allowedRevisionExtensions = { ".txt", ".doc", ".docx", ".pdf", ".pptx", ".ppt"  };

        public MaterialService(IFileService fileService , IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _materialRepository = _unitOfWork.repository<Material>();
        }

        public async Task< string> UploadMaterial(IFormFile File , MaterialType materialType, MaterialAddDto dto)
        {
            string[] allowedExtension= allowedRevisionExtensions;
            string subfolder = "";

            if (materialType==MaterialType.Video)
            {
                allowedExtension = allowedVideoExtensions;
                subfolder = "/Video";

            }
            else if(materialType == MaterialType.Exam)
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
            var filePath = await _fileService.SaveFileAsync(File, allowedExtension, BaseSubFolder + subfolder, false) ;
            if (string.IsNullOrEmpty(filePath))
            {
                return null; 
            }

            var fileName = Path.GetFileName(filePath);

            
            var uploadedMaterial = new Material
            {
                MaterialName = fileName,
                SubjectId = dto.SubjectId,
                TeacherId = dto.TeacherId,
                Type = materialType,

            };
            await _materialRepository.Add(uploadedMaterial);
            foreach(int _class in dto.MaterialClasses){
                _unitOfWork.repository<ClassMaterial>()
                    .AddWithoutSave(new ClassMaterial{ 
                        ClassId = _class, MaterialId = uploadedMaterial.Id
                    });
            }
            await _unitOfWork.CompleteAsync();
            return fileName;
        }
        public string GetFilePath(string fileName, MaterialType materialType)
        {

            string subfolder = "";
            if (materialType == MaterialType.Video)
                subfolder = "/Video";
            else if (materialType != MaterialType.Exam)
                subfolder = "/Exam";
            else if (materialType != MaterialType.Summary)
                subfolder = "/Summary";
            else if (materialType!= MaterialType.Revision)
                subfolder = "/Revision";
            else if (materialType != MaterialType.Book)
                subfolder = "/Book";
            return Path.Combine(BaseSubFolder + subfolder, fileName);
        }
    }
}
