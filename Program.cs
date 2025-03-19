using InverntorySys.Data;

namespace InverntorySys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<ItemService>(); // Register JSON file-based data service

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSession(); // Add session support
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Allow access in _Layout.cshtml


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession(); // Enable session middleware
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
