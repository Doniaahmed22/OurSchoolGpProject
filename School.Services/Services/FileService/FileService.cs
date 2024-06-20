using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
namespace School.Services.Services.FileService
{
    public class FileService: IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> SaveFileAsync(IFormFile File, string[] allowedFileExtensions ,string subfolder,bool AllowedRepeatedFilename )
        {
            if (File == null)
            {
                throw new ArgumentNullException(nameof(File));
            }

            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, subfolder);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Check the allowed extenstions
            var ext = Path.GetExtension(File.FileName);
            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
            }

            // generate a unique filename
            string fileNameWithPath;
            if (AllowedRepeatedFilename)
            {
                var fileName = $"{Guid.NewGuid().ToString()}{ext}";
                fileNameWithPath = Path.Combine(path, fileName);
            }
            else
            {
                var fileName = File.FileName;
                fileNameWithPath = Path.Combine(path, fileName);
                if (System.IO.File.Exists(fileNameWithPath))
                   return null;
            }
         
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await File.CopyToAsync(stream);
            return fileNameWithPath;
        }


        public async Task<(MemoryStream stream, string contentType, string fileName)> DownloadFileAsync(string sub)
        {

            var contentPath = _environment.ContentRootPath;
            var filePath = Path.Combine(contentPath, sub);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var contentType = GetContentType(filePath);
            return (memory, contentType, Path.GetFileName(filePath));
        }

        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
            {
               { ".pdf", "application/pdf" },
               { ".txt", "text/plain" },
               { ".doc", "application/msword" },
               { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
               { ".xls", "application/vnd.ms-excel" },
               { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
               { ".png", "image/png" },
               { ".jpg", "image/jpeg" },
               { ".jpeg", "image/jpeg" },
               { ".gif", "image/gif" },
               { ".csv", "text/csv" },
               { ".mp4", "video/mp4" },
               { ".avi", "video/x-msvideo" },
               { ".mov", "video/quicktime" },
               { ".wmv", "video/x-ms-wmv" },
               { ".epub", "application/epub+zip" },
               { ".mobi", "application/x-mobipocket-ebook" }
            };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }


        public void DeleteFile(string subpath)
        {
            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, subpath);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid file path");
            }
            File.Delete(path);
        }
    }
}
