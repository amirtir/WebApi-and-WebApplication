using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MyWeb.Models;
using MyWeb.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyWeb
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<EshopApi_DBContext>(options => { options.UseSqlServer("Data Source=.;Initial Catalog=EshopApi_DB;Integrated Security=True;"); });
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISalePersonRepository, SalePersonRepository>();
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {

                    Title = "First Swagger",
                    Version = "3.0.0"

                });
            
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44305",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ValidationKeyApiVerify"))

                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCores", builder =>

                 builder
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials()
                 .Build()
                  );

            });

        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseCors("EnableCores");
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.jason", "First Swagger"); });
            app.UseAuthentication();
            app.UseMvc();

        }
    }
}

