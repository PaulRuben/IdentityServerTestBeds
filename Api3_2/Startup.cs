﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api3_2
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddAuthorization();

            services.AddWebEncoders();
            services.AddCors();
            services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // this uses the policy called "default"
            //       app.UseCors("default");

            app.UseCors(policy =>
            {
                policy.WithOrigins(
                    "http://localhost:2235");

                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("WWW-Authenticate");
            });

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "https://localhost:44300", // Changed from http://localhost:5000 to https://localhost:44300 to verify that a .NET Core app can use IdentityServer3 in place of IdentityServer4
                RequireHttpsMetadata = false,
                EnableCaching = false,
                LegacyAudienceValidation = true,
                ApiName = "api3_2",
            });

            app.UseMvc();
        }
    }
}