using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Training.Lib.DataModel;
using Training.Lib.Services;

namespace Training.API
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
            //Add scoped training service to IoC (as training service class uses data context class so need to be added as scoped)
            services.AddScoped<ITrainingService, TrainingService>()
                    .AddDbContext<TrainingDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WDSConnectionString")));


            services.AddControllers();
            //Configure localization for AU dateformat
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-AU");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-AU") };
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            //Add Swagger specification to the IoC.
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "WDSTrainingAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "WDS Training API",
                        Version = "1"
                    });
                var commentfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                commentfile = Path.Combine(AppContext.BaseDirectory, commentfile);
                setupAction.IncludeXmlComments(commentfile);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TrainingDBContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Add database creation if not exist to request pipeline
            context.Database.EnsureCreated();


            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();
            //Add swagger UI to pipeline and remove the swagger prefix
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/WDSTrainingAPISpecification/swagger.json", "WDS Training API");
                setupAction.RoutePrefix = "";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
