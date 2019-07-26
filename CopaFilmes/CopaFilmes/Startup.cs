using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaFilmes.Domain.Repositories.Impl.Contexts;
using CopaFilmes.Domain.Repositories.Impl.Repositories;
using CopaFilmes.Domain.Repositories.Interfaces;
using CopaFilmes.Domain.Services.Impl.Services;
using CopaFilmes.Domain.Services.Interfaces;
using CopaFilmes.Services.Impl.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CopaFilmes
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddScoped<ApplicationContext, ApplicationContext>();
            services.AddTransient<IFilmeRepository, FilmeRepository>();
            services.AddTransient<IFilmeService, FilmeService>();
            services.AddTransient<ICampeonatoService, CampeonatoService>();
        }

        private void SeedDatabase(IApplicationBuilder app, IHostingEnvironment env)
        {
            var serviceFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = serviceFactory.CreateScope())
            {
                Task.WhenAll(
                  PrepareDatabaseAsync(scope.ServiceProvider, env)
                ).Wait();
            }
        }

        private async Task PrepareDatabaseAsync(IServiceProvider serviceProvider, IHostingEnvironment env)
        {
            var dbContext = serviceProvider.GetService<ApplicationContext>();

            if (await dbContext.Database.EnsureCreatedAsync())
            {
                await dbContext.SeedData();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            SeedDatabase(app, env);

            /* app.UseSwagger();

             app.UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
             });*/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
