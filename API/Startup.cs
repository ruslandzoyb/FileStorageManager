using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Configuration.TokenServices;
using BL.Interfaces.OrdersInterfaces;
using BL.Services.CommonServices;
using DAL.Context;
using DAL.Interfaces.UnitOfWork;
using DAL.Models.IdentityModels;
using DAL.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.IdentityModel.Tokens;

namespace API
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



            services.AddIdentityCore<ApplicationUser>().
                AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();
            
            //services.AddDbContext<IdentityContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            BL.Configuration.Injection.InfrastructureConfiguration.Configure(services);
            BL.Configuration.Injection.InfrastructureConfiguration.
                DbContext(services,Configuration.GetConnectionString("FilesDb"), Configuration.GetConnectionString("IdentityConnection"));

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
            //  services.AddTransient<IAdminService, AdminService>();


            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                   
                    options.SaveToken = false;
                    
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "API",
                        ValidateAudience = true,
                        ValidAudience = "Postman",
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
            });

         //   services.AddSingleton<UserManager<ApplicationUser>>();
           // services.AddTransient<IAccountService, AccountService>();




            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;

                
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          
            app.UseRouting();
            // app.UseAuthentication();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
