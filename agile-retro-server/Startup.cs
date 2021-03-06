using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileRetroServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AgileRetroServer
{
    public class Startup
    {
        public class CorsSettings 
        {
            public IEnumerable<string> AllowedOrigins {get; set;}
        }

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "agile_retro_server", Version = "v1" });
            });

            var corsSettings = new CorsSettings();
            Configuration.GetSection("Cors").Bind(corsSettings);
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IItemRepository, ItemRepository>();
            services.AddCors(options => options
                .AddDefaultPolicy(builder => builder
                    .SetIsOriginAllowed(origin => corsSettings.AllowedOrigins.Contains(origin))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "agile_retro_server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
