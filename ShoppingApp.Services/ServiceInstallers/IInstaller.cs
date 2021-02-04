using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingApp.Services.ServiceInstallers
{
    public interface IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services);
    }
}
