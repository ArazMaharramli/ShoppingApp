using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.AuthServices.JwtTokenServices;
using ShoppingApp.Services.AuthServices.JwtTokenServices.Options;
using ShoppingApp.Services.ServiceInstallers;

namespace ShoppingApp.Web.API.ServiceInstallers
{
    public class JwtTokenServiceInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<JwtOptions>(configuration.GetSection("JwtDefaults"));

            services.AddSingleton<IJwtTokenService, JwtTokenService>();
        }
    }
}
