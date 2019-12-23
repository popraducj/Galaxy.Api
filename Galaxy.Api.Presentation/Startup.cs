using System;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Presentation.Ioc;
using Galaxy.Api.Presentation.Middleware;
using Galaxy.Teams.Core.Models.Settings;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddServices();
            services.AddUserAuthentication();
            services.AddControllers();
            services.RegisterTypes();
            services.AddGraphQl();
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddGraphQlAuth();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger.CreateLogger("Galaxy.Api.GlobalLogger"));
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseGraphQL<ISchema>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
