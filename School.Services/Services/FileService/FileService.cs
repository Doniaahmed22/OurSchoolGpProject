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

            



        public void DeleteFile(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }
            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, $"Uploads", fileNameWithExtension);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid file path");
            }
            File.Delete(path);
        }
    }
}
