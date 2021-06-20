using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Services.FileServices
{
    public class FileService : IFileService
    {
        public bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<string> UploadFileAsync(IFormFile file, string root, string folder, string fileName)
        {
            if (file != null)
            {

                string uploadsFolder = Path.Combine(root, "uploads", folder);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var extension = file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                var uniqueFileName = fileName + $"_{Guid.NewGuid().ToString("N")} " + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Path.Combine("uploads", folder, uniqueFileName);
            }

            return null;
        }
    }
}
