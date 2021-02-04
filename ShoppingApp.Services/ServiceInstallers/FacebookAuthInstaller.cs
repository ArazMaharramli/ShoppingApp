using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.AuthServices.FacebookAuthService;
using ShoppingApp.Services.AuthServices.FacebookAuthService.Options;

namespace ShoppingApp.Services.ServiceInstallers
{
    public class FacebookAuthInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            //var facebookOptions = new FacebookAuthOptions();
            //configuration.Bind("ExternalAuthSettings:Facebook", facebookOptions);

            services.Configure<FacebookAuthOptions>(configuration.GetSection("ExternalAuthSettings:Facebook"));

            services.AddSingleton<IFacebookAuthService, FacebookAuthService>();
        }
    }
}
