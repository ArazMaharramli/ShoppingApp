using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.ServiceInstallers;
using ShoppingApp.Web.API.Contracts.Validators;

namespace ShoppingApp.Web.API.ServiceInstallers
{
    public class MVCInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc()
                .AddFluentValidation(configuration =>
                    configuration.RegisterValidatorsFromAssembly(typeof(LoginWithExternalProviderRequestModelValidator).Assembly));
        }
    }

}
