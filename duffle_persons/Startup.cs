#region Copyright (c) 2016 Periwinkle Software Limited
// MIT License -- https://opensource.org/licenses/MIT
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
// associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, 
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

namespace duffle_persons
{
    using System;
    using System.IO;
    using Boilerplate;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Processors.Implementations;
    using Processors.Interfaces;
    using Repositories.Implementations;
    using Repositories.Interfaces;
    using Serilog;
    using Swashbuckle.Swagger.Model;

    public class Startup
    {
        private IHostingEnvironment env { get; set; }
        public Startup(IHostingEnvironment env)
        {
            this.env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            BuildDIRoot(services);

            services.AddMvc();
            services.AddMvc().AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
# if DEBUG
                    options.SerializerSettings.Formatting = Formatting.Indented;
#else
                    options.SerializerSettings.Formatting = Formatting.None;
#endif
                });

            services.AddSwaggerGen(c =>
                {
                    c.SingleApiVersion(new Info
                    {
                        Version = MicroserviceInfo.MicroserviceVersion,
                        Title = "DUFFLE People API",
                        Description = "This API manages DUFFLE people, both DRT personnel as well as animal owners/agents.",
                    });
                    //c.OperationFilter<AssignOperationVendorExtensions>();
                });

            services.ConfigureSwaggerGen(c => { c.IncludeXmlComments(GetXmlCommentsPath()); });
        }

        private void BuildDIRoot(IServiceCollection services)
        {
            services.AddSingleton<IRepositoryIdentity, RepositoryIdentity>();
            services.AddSingleton<IOwnersRepository, OwnersRepository>();
            services.AddTransient<IMicroserviceInfo, MicroserviceInfo>();
            services.AddTransient<IOwnersProcessor, OwnersProcessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            Log.Logger = new LoggerConfiguration().WriteTo.RollingFile(@"Log-{Date}.txt").CreateLogger();

            app.UseMvc();
            app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseStaticFiles();

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions());
            }

            app.UseSwagger((httpRequest, swaggerDoc) =>
             {
                 swaggerDoc.Host = httpRequest.Host.Value;
             });
            app.UseSwaggerUi();
        }

        private static string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, app.ApplicationName + ".xml");
        }
    }
}
