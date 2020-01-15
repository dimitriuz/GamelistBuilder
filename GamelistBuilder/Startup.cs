using GamelistBuilder.Infrastructure;
using GamelistBuilder.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GamelistBuilder
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IConfigurationRoot>(Configuration);

            services.AddDbContext<GamelistBuilderContext>();
            services.AddTransient<IRepository<Platform>, PlatformRepository>();
            services.AddTransient<IRepository<Gamelist>, GamelistRepository>();
            services.AddTransient<IRepository<Game>, GameRepository>();
            services.AddTransient<IRepository<GameFolder>, GameFolderRepository>();
            services.AddSingleton<ScraperSS, ScraperSS>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            

            services.AddHttpClient();
            services.AddRazorPages();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gamelist API", Version = "v1" });
            });

            //services.AddMvc()
            //    .AddRazorOptions(options =>
            //{
            //    // Clear the current list of view location formats. At this time, 
            //    // the list contains default view location formats.
            //    options.ViewLocationFormats.Clear();

            //    // {0} - Action Name
            //    // {1} - Controller Name
            //    // {2} - Area Name
            //    options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
            //    options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //    options.ViewLocationFormats.Add("/Views/Shared/Layouts/{0}.cshtml");
            //    options.ViewLocationFormats.Add("/Views/Shared/PartialViews/{0}.cshtml");
            //}); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var log = logger.CreateLogger("Main");

            //app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gamelist API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

            DBInitializer.Seed(app);

        }
    }
}
