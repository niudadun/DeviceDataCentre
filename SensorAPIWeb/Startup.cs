using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SensorAPIWeb.Domain;
using SensorAPIWeb.Domain.DBRepositories;
using SensorAPIWeb.MiddleWare;
using SensorAPIWeb.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SensorAPIWeb
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
            services.AddScoped<IDeviceUpdateRepository, DeviceUpdateRepository>();
            services.AddScoped<IGetDeviceDetailsRepository, GetDeviceDetailsRepository>();
            services.AddScoped<ISecurityCheckRepository, SecurityCheckRepository>();
            services.AddScoped<IFileLoadRepository, FileLoadRepository>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Automatically perform database migration 
            services.AddDbContext<SensorAPIDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("SensorAPIDatabase")));
            services.ConfigureCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "You api title", Version = "v1" });
                c.AddSecurityDefinition("Token", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Token", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Token", Enumerable.Empty<string>() },
            });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseTokenAuth();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
        }

    }
    public static class ServiceExtensions
    {
        /// <summary>
        /// cross domain policy configuration
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
    }
}
