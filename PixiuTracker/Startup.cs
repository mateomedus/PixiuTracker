using DatabaseContext;
using Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PixiuTracker.Helpers;

namespace PixiuTracker
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
            services.AddCors();
            services.AddControllers();
            services.AddDatabaseAccess(Configuration["ConnectionString"]);
            services.AddMapperServices();
            services.AddScoped<JwtService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PixiuTracker", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PixiuTracker v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(p => p
                .WithOrigins(new[] { "http://localhost:3000" })
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );
           /* app.UseCors(options => options
                .WithOrigins(new[] {"https://localhost:3000"})
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
               ); 
           */
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
