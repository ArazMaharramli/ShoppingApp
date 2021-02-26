using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Services.FileServices
{
    public interface IFileService
    {
        public Task<string> UploadFileAsync(IFormFile file, string root, string folder, string fileName);
        public bool DeleteFile(string path);
    }
}

