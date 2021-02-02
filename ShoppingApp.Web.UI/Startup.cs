using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingApp.CQRS.Handlers;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.UnitOFWork.Persistence;
using ShoppingApp.UnitOFWork.Repositories;
using MediatR;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.DBServices.SqlDBServices;
using ShoppingApp.Services.AuthServices.FacebookAuthService;
using ShoppingApp.Services.AuthServices.FacebookAuthService.Options;
using ShoppingApp.Services.AuthServices.GoogleAuthService.Options;
using ShoppingApp.Services.AuthServices.GoogleAuthService;

namespace ShoppingApp.Web.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShoppingAppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", config =>
                {
                    config.Cookie.Name = "ShoppingApp";
                });

            services.AddIdentity<User, Role>(config =>
            {
                config.Password.RequireDigit = Configuration.GetValue<bool>("PasswordSettings:RequireDigit");
                config.Password.RequireLowercase = Configuration.GetValue<bool>("PasswordSettings:RequireLowercase");
                config.Password.RequireNonAlphanumeric = Configuration.GetValue<bool>("PasswordSettings:RequireNonAlphanumeric");
                config.Password.RequireUppercase = Configuration.GetValue<bool>("PasswordSettings:RequireUppercase");
                config.Password.RequiredLength = Configuration.GetValue<int>("PasswordSettings:RequiredLength");
                config.SignIn.RequireConfirmedEmail = Configuration.GetValue<bool>("PasswordSettings:RequireConfirmedEmail");
            })
                    .AddEntityFrameworkStores<ShoppingAppDbContext>()
                    .AddRoles<Role>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddMediatR(typeof(LoginWithFacebookCommandHandler));
            services.AddTransient<IUserIdentityService, UserIdentityService>();

            // bunlari cqrs islesin deye yazdim cqrs modifikasiya olunmalidi mence
            //burdan
            services.AddHttpClient();
            var facebookOptions = new FacebookAuthOptions();
            Configuration.Bind("ExternalAuthSettings:Facebook", facebookOptions);
            services.AddSingleton(facebookOptions);
            services.AddSingleton<IFacebookAuthService, FacebookAuthService>();

            var googleOptions = new GoogleAuthOptions();
            Configuration.Bind("ExternalAuthSettings:Google", facebookOptions);
            services.AddSingleton(googleOptions);
            services.AddSingleton<IGoogleAuthService, GoogleAuthService>();
            // bura qeder
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
