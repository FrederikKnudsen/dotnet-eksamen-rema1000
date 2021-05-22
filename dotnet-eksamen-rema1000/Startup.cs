using dotnet_eksamen_rema1000.Infrastructure;
using dotnet_eksamen_rema1000.Models;
using dotnet_eksamen_rema1000.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_eksamen_rema1000
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
            services.AddDbContext<DatabaseContext>();

            services.AddScoped<CategoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<SupplierService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = ".Net Eksamen Rema1000", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                SeedData(serviceProvider);
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet_eksamen_rema1000 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void SeedData(IServiceProvider serviceProvider)
        {
            var products = new List<Product>();

            for (int i = 0; i < 10; i++)
            {
                products.Add(new Product()
                {
                    ID = 1 + i,
                    Description = "Product description",
                    Amount = 10 + i,
                    Category = new Category()
                    {
                        ID = 1 + i,
                        Description = "Generic category description",
                        Name = "Category name"
                    },
                    Name = "Product name",
                    Price = 12 * i,
                    StockAmount = 10 + i,
                    Supplier = new Supplier()
                    {
                        ID = 1 + i,
                        Address = "Fake address 123",
                        ContactPerson = "Fake person",
                        Email = "fake@email.com",
                        Name = "Fake supplier name",
                        Phone = 12345678,
                        ZipCode = 123
                    },
                    Unit = "gram"
                });
            }

            var context = serviceProvider.GetService<DatabaseContext>();

            context?.AddRange(products);
            context?.SaveChanges();
        }
    }
}
