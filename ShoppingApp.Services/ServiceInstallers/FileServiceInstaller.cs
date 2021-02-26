using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.EmailServices;
using ShoppingApp.Services.FileServices;

namespace ShoppingApp.Services.ServiceInstallers
{
    public class FileServiceInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<SmtpOptions>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IFileService, FileService>();
        }
    }
}
