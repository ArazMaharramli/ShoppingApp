using System;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.CQRS.Handlers;

namespace ShoppingApp.Web.API.ServiceInstallers
{
    public class MediatrInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            var type = typeof(LoginWithFacebookCommandHandler);
            services.AddMediatR(type);
        }
    }
}
