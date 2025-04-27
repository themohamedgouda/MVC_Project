using BusinessLogic.Profiles;
using BusinessLogic.Services.AttachmentService;
using BusinessLogic.Services.Classes;
using BusinessLogic.Services.Interfaces;
using DataAccess.Data.Contexts;
using DataAccess.Models.IdentityModel;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using MailKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Presentation.Helper;
using Presentation.Settings;
using Presentation.Utilities;
using Vonage;
using Vonage.Request;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            #region Services
            builder.Services.AddControllersWithViews(options=>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            //builder.Services.AddScoped<ApplicationDbContext>(); // Register to Services in DI Contianer
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
             },ServiceLifetime.Scoped); 
            
            //builder.Services.AddScoped<DepartmentRepository>();
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices,DepartmentServices>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddTransient<IMailServices, MailServices>();
            builder.Services.AddScoped<IVonageSmsService, VonageSmsService>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //builder.Services.AddAutoMapper(typeof(MappingProfiles));        
            //builder.Services.AddAutoMapper(typeof(ProjectReference).Assembly);        
            #endregion
            // Add middleware to the container.
            #region MiddleWare
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.ExpireTimeSpan = TimeSpan.FromDays(2);
                config.LoginPath = "/Account/Login";
                config.LogoutPath = "/Account/SignOut";
                config.AccessDeniedPath ="/Home/Error";
             });
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.Configure<VonageSettings>(builder.Configuration.GetSection("VonageSettings"));

            // تسجيل VonageClient في DI Container
            builder.Services.AddSingleton<VonageClient>(provider =>
            {
                var settings = provider.GetRequiredService<IOptions<VonageSettings>>().Value;

                var credentials = Credentials.FromApiKeyAndSecret(
                    settings.ApiKey,
                    settings.ApiSecret
                );

                return new VonageClient(credentials);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
            #endregion
            app.Run();
        }
    }
}
// Steps To integrate with any External Services
/*
    1- App Settings handling
    2- Install package
    3- Account Controller
 */