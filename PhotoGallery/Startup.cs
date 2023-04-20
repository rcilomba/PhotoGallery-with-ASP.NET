using PhotoGallery.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;
using PhotoGallery.Models;
using Microsoft.AspNetCore.Http;

namespace PhotoGallery
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
        Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRazorPages();
            services.AddTransient<JsonFilePhotoService>();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();

            // Configure the PhotoGallery.Services namespace
            services.AddSingleton<IFileProvider>(
  new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            // Configure the PhotoGallery.Models namespace
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Index", "");
            });

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                /*endpoints.MapGet("/photos", (context) =>
                {
                    var photos = app.ApplicationServices.GetService<JsonFilePhotoService>().GetProducts();
                    var json = JsonSerializer.Serialize<IEnumerable<Photo>>(photos);
                    return context.Response.WriteAsync(json);
                }); */
            });
        }
    }
}