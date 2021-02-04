using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.EmailServices;

namespace ShoppingApp.Services.ServiceInstallers
{
    public class EmailServiceInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<SmtpOptions>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
