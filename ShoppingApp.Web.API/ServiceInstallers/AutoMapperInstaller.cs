using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingApp.Web.API.ServiceInstallers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}
