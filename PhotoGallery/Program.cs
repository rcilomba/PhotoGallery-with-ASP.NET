using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PhotoGallery;
using PhotoGallery.Services;


namespace PhotoGallery
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }

    public class Startup
    {

        public Startup(IConfiguration configuration) =>
        Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();
            services.AddScoped<JsonFilePhotoService>();
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

            // 404 blazor.server.js
            app.UseStaticFiles();

            app.UseHttpsRedirection();
          
            app.UseRouting();

            app.UseAuthorization();
            app.Use(async (context, next) => { using var serviceScope = app.ApplicationServices.CreateScope(); 
                var photoService = serviceScope.ServiceProvider.GetService<JsonFilePhotoService>(); 
                if (photoService == null) { throw new Exception("PhotoService is not registered."); } await next.Invoke(); });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();


                // endpoints.MapGet("/photos", (context) =>
                // {
                //    var photos = app.ApplicationServices.GetService<JsonFilePhotoService>().GetPhotos();
                //  var json = JsonSerializer.Serialize<IEnumerable<Photo>>(photos);
                //   return context.Response.WriteAsync(json);
                // }); 


            });
        }
    }
}