using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Services.FileServices
{
    public interface IFileService
    {
        public Task<string> UploadFileAsync(IFormFile file, string folder, string name, string fileNameType = "CompanyRegistrationCertificate");
        public bool DeleteFile(string path);
    }
}

