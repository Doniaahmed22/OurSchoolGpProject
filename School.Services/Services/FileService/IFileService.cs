using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.FileService
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile File, string[] allowedFileExtensions, string subfolder, bool AllowedRepeatedFilename);
        Task<(MemoryStream stream, string contentType, string fileName)> DownloadFileAsync(string filePath);

        void DeleteFile(string fileNameWithExtension);
    }
}
