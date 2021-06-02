using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Interfaces;
using ESC_MANEJO.CORE.Services;
using ESC_MANEJO.INFRASTRUCTURE.Repositories;
using ESC_MANEJO.WEB.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESC_MANEJO.WEB
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
            services.Configure<ConfigurationLog>(Configuration.GetSection("LogService"));
            services.Configure<ConfigurationMessages>(Configuration.GetSection("MessagesDefault"));
            services.Configure<ConfigurationRepository>(Configuration.GetSection("RepositoryConfiguration"));
            services.Configure<ConfigurationWeb>(Configuration.GetSection("WebConfiguration"));

            services.AddTransient<IParseService, ParseService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IHttpService, HttpService>();
            
            services.AddTransient<ICryptoService, CryptoService>();

            services.AddTransient<IAdminService, AdminService>();

            services.AddTransient<IAdminRepository, AdminRepository>();
            
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
