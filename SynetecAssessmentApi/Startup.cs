using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Persistence.Interfaces;
using SynetecAssessmentApi.Persistence.Repositories;
using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Services.Interfaces;

namespace SynetecAssessmentApi
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SynetecAssessmentApi", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "HrDb"));

            //Repositories
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            //Services
            services.AddTransient<IMappingService, MappingService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ICalculationService, CalculationService>();
            services.AddTransient<IBonusPoolService, BonusPoolService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SynetecAssessmentApi v1"));
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
