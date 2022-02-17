using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DataLayer.Repository;
using DataLayer;
using BusinessUnit.Services.Interfaces;
using BusinessUnit.Services;

namespace TestJunior
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
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "MyPolicy", builder =>
                    {
                        builder
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin();
                    });
            });

            services.AddControllers();
            services.AddScoped<IRepository<Brand>, BrandRepository>();
            services.AddScoped<IBrandServices, BrandServices>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IRepository<InfoRequest>, InfoRequestRepository>();
            services.AddScoped<IRequestServices, RequestServices>();
            services.AddDbContextPool<DatabaseContext>(optionsBuilder =>
            {
                string ConnectionString = Configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlServer(ConnectionString);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
