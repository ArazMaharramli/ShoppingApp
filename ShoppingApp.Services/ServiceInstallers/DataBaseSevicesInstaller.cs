﻿using Microsoft.Extensions.Configuration;
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
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<ISizeService, SizeService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ITagService, TagService>();
        }
    }
}
