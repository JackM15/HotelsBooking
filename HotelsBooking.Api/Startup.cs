using HotelsBooking.Dal;
using HotelsBooking.Dal.Repositories;
using HotelsBooking.Domain.Abstractions.Repositories;
using HotelsBooking.Domain.Abstractions.Services;
using HotelsBooking.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace HotelsBooking.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelsBooking.Api", Version = "v1" });
            });
            //Create a single data source instance
            services.AddSingleton<DataSource>();
            //EF Core
            var connString = Configuration.GetConnectionString("Default");
            services.AddDbContext<DataContext>(options => { options.UseSqlServer(connString);});
            //Automapper
            services.AddAutoMapper(typeof(Startup));
            //Implement Repository
            services.AddScoped<IHotelsRepository, HotelsRepository>();
            //Add Reservation Service
            services.AddScoped<IReservationService, ReservationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelsBooking.Api v1"));
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
