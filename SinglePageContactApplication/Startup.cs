using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SinglePageContactApplication.Models.Data;
using SinglePageContactApplication.RuntimePlugins;

namespace SinglePageContactApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ContactDbContext>(options =>
            {
                options.UseMySql(this.Configuration.GetConnectionString(DataBaseComponentNames.DataBase),
                    new MySqlServerVersion(new Version(8, 0, 27)));
            });

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(Routes.ErrorPageRoute);
                
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}