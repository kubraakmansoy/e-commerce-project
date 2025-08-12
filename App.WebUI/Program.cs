using System.Net.Http;
using System.Net.Http.Json;
namespace App.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var apiBaseUrl = builder.Configuration["Apis:Data"];
            if (string.IsNullOrWhiteSpace(apiBaseUrl))
                throw new InvalidOperationException("Apis:Data ayarý bulunamadý. appsettings.Development.json'a ekleyin.");

            // 2) Named HttpClient ekle
            builder.Services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl); // ör: http://localhost:5028/
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.Run();
        }
    }
}



