using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.UserModels;

namespace ShoppingApp.Services.ServiceInstallers
{
    public class IdentityServicesInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddIdentity<User, Role>(config =>
            {
                config.Password.RequireDigit = configuration.GetValue<bool>("PasswordSettings:RequireDigit");
                config.Password.RequireLowercase = configuration.GetValue<bool>("PasswordSettings:RequireLowercase");
                config.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("PasswordSettings:RequireNonAlphanumeric");
                config.Password.RequireUppercase = configuration.GetValue<bool>("PasswordSettings:RequireUppercase");
                config.Password.RequiredLength = configuration.GetValue<int>("PasswordSettings:RequiredLength");
                config.SignIn.RequireConfirmedEmail = configuration.GetValue<bool>("PasswordSettings:RequireConfirmedEmail");

                config.Lockout.AllowedForNewUsers = false;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                config.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddEntityFrameworkStores<ShoppingAppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
