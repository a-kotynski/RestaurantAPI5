using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestaurantAPI5.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI5
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
            //services.AddSingleton<>(); // registering dependencies with AddSingleton<>() we're sure a certain dependecy has been created only once during 
            //services.AddScoped<>; //tl;dr: one request = one instance of a service // registering dependecies with different durations of objects; each object will be created anew for each requst sent by a client
            services.AddTransient<IWeatherForecastService, WeatherForecastService>(); // same as scoped, but objects are created each time we call them through a constructor
            services.AddControllers();
            services.AddDbContext<RestaurantDbContext>();
            services.AddScoped<RestaurantSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantSeeder seeder)
        {
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
