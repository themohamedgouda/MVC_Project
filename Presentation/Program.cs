using BusinessLogic.Services;
using DataAccess.Data.Contexts;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            #region Services
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<ApplicationDbContext>(); // Register to Services in DI Contianer
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            #region ConnectionStrings
             //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
             //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"])
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) 
            #endregion
            );
            //builder.Services.AddScoped<DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices,DepartmentServices>();
            #endregion
            // Add middleware to the container.
            #region MiddleWare
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion
            app.Run();
        }
    }
}
