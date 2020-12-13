using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Carpool.Infrastructure.EfModels;
using Carpool.Core.UnitOfWork;
using Carpool.Infrastructure.EfUnitOfWork;
using Carpool.Core.Services.Contracts;
using Carpool.Core.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Carpool.Core.Requests;
using Carpool.Core.Validators;
using System;

namespace Carpool.Api
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

            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Carpool.Api", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        //builder.WithOrigins("http://localhost:3000")
                        builder.AllowAnyOrigin()
                            //.SetIsOriginAllowed(x => _ = true)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddDbContext<CarpoolContext>();

            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITravelPlanService, TravelPlanService>();

            services.AddTransient<IValidator<TravelPlanCreateRequest>, TravelPlanRequestValidator<TravelPlanCreateRequest>>();
            services.AddTransient<IValidator<TravelPlanUpdateRequest>, TravelPlanRequestValidator<TravelPlanUpdateRequest>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carpool.Api v1"));
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedData.SaveInitialData();
        }
    }
}
