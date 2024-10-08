using Microsoft.EntityFrameworkCore;
using myshop.DataAccess.Data;
using myshop.DataAccess.Implementation;
using myshop.Entities.Repositories;

namespace myshop.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

        builder.Services.AddDbContext<ApplicationDbContext>(option=>option.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
        ));

        builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

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

        // Route for the Admin area
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        // Default route (when area is not specified)
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}