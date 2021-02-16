using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.DBServices.DBServicesImplementations;

namespace ShoppingApp.Services.ServiceInstallers
{
    public class DataBaseSevicesInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddTransient<IUserIdentityService, UserIdentityService>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IStoreTypeService, StoreTypeService>();
            services.AddTransient<IDeliveryOptionService, DeliveryOptionService>();
            services.AddTransient<ICountryService, CountryService>();
        }
    }
}
