using GamelistBuilder.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Infrastructure
{
    public static class DBInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<GamelistBuilderContext>();
                context.Database.EnsureCreated();
                if (context.Platforms.Count() == 0)
                {
                   string path = app.ApplicationServices.GetService<IConfiguration>().GetSection("PlatformsPath").Value;
                   var platforms = PlatformHelper.ImportPlatforms(path);
                   foreach (var platform in platforms)
                    {
                        context.Platforms.Add(platform);
                    }; 
                };

                context.SaveChanges();
            }
            }
        }
}
