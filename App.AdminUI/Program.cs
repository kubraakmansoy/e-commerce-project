namespace App.AdminUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // API base url
            var apiBaseUrl = builder.Configuration["Apis:Data"]
                ?? throw new InvalidOperationException("Apis:Data missing");

            // Named HttpClient
            builder.Services.AddHttpClient("api", c => c.BaseAddress = new Uri(apiBaseUrl));

            // (Ýleride kimlik doðrulama ekleyeceðiz)


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
