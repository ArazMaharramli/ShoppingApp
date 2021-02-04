using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Services.ServiceInstallers;

namespace ShoppingApp.Web.API.ServiceInstallers
{
    public class AuthenticationServiceInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
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
