using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TextEditor_Final.Areas.Identity.Data;
namespace TextEditor_Final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("TextEditorContextConnection") ?? throw new InvalidOperationException("Connection string 'TextEditorContextConnection' not found.");

                                    builder.Services.AddDbContext<TextEditorContext>(options =>
                options.UseSqlServer(connectionString));

                                                builder.Services.AddDefaultIdentity<TextEditorUser>()
                .AddEntityFrameworkStores<TextEditorContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}