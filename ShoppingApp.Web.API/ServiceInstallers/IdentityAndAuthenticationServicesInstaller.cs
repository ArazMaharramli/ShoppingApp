using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain;
using ShoppingApp.Domain.Models.Domain.UserModels;

namespace ShoppingApp.Web.API.ServiceInstallers
{
    public class IdentityAndAuthenticationServicesInstaller : IInstaller
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
            })
                .AddEntityFrameworkStores<ShoppingAppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    SaveSigninToken = true,
                    ValidateIssuer = configuration.GetValue<bool>("JwtDefaults:ValidateIssuer"),
                    ValidateAudience = configuration.GetValue<bool>("JwtDefaults:ValidateAudience"),
                    ValidAudience = configuration.GetValue<string>("JwtDefaults:ValidAudience"),
                    ValidIssuer = configuration.GetValue<string>("JwtDefaults:ValidIssuer"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtDefaults:SecurityKey"))),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                };
            });


        }
    }
}
