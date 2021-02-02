using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.AuthServices.GoogleAuthService;
using ShoppingApp.Services.AuthServices.GoogleAuthService.Options;

namespace ShoppingApp.Web.API.ServiceInstallers
{
    public class GoogleAuthInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            //var googleOptions = new GoogleAuthOptions();
            //configuration.Bind("ExternalAuthSettings:Google", googleOptions);

            services.Configure<GoogleAuthOptions>(configuration.GetSection("ExternalAuthSettings:Google"));

            services.AddSingleton<IGoogleAuthService, GoogleAuthService>();
        }
    }
}
