using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Domain.Data;
using ShoppingApp.Web.API.Contracts.Validators;

namespace ShoppingApp.Web.API.ServiceInstallers
{
    public class MVCControllerAndDBServiceInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ShoppingAppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc()
                .AddFluentValidation(configuration =>
                    configuration.RegisterValidatorsFromAssembly(typeof(LoginWithExternalProviderRequestModelValidator).Assembly));
        }
    }
}
