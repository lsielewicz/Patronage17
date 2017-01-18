using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Patronage17.Engine.Helpers;
using Microsoft.AspNetCore.Routing;
using System.IO;

namespace Patronage17.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuilder = new RouteBuilder(app);
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            routeBuilder.MapGet("", context => 
            {
                var files = FilesIoHelper.Instance.GetFiles(appLocation);
                var output = string.Join(Environment.NewLine, files);

                return context.Response.WriteAsync(output);
            });

            routeBuilder.MapGet("{fileName}", context =>
            {
                var fileName = context.GetRouteValue("fileName").ToString();
                var filePath = Path.Combine(appLocation, fileName);
                var fileInfo = FilesIoHelper.Instance.GetFileMetadata(filePath);

                if (fileInfo != null)
                {
                    var output = $"{"File name:",-25} {fileInfo.Name}{Environment.NewLine}" +
                                 $"{"Extension:",-25} {fileInfo.Extension}{Environment.NewLine}" +
                                 $"{"Last write time:",-25} {fileInfo.LastWriteTime}{Environment.NewLine}" +
                                 $"{"Creation time:",-25} {fileInfo.CreationTime}{Environment.NewLine}";

                    return context.Response.WriteAsync(output);
                }
                return context.Response.WriteAsync($"Couldn't get the information about {context.GetRouteValue("fileName")}");
            });
        

            app.UseRouter(routeBuilder.Build());

        }
    }
}
