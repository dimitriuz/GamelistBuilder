using GamelistBuilder.Infrastructure;
using GamelistBuilder.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace GamelistBuilder
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            //var dom = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("MyAppSettings.json")
            //    .AddInMemoryCollection(new Dictionary<string, string> { { "Timezone", "+1" } })
            //    .AddEnvironmentVariables()
            //    .Build();

            //// Save the configuration root object to a startup member for further references
            //Configuration = dom;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IConfigurationRoot>(Configuration);

            services.AddTransient<IXMLRepository<Platform>, PlatformRepository>();
            services.AddTransient<IRepository<Gamelist>, GamelistRepository>();


            services.AddMvc()
                .AddRazorOptions(options =>
            {
                // Clear the current list of view location formats. At this time, 
                // the list contains default view location formats.
                options.ViewLocationFormats.Clear();

                // {0} - Action Name
                // {1} - Controller Name
                // {2} - Area Name
                options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/Layouts/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/PartialViews/{0}.cshtml");
            }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {

            logger.AddConsole();
            logger.AddDebug();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/app/error/{0}");
            }
        }
    }
}
