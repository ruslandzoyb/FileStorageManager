using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL.Configuration.TokenServices;
using BL.Interfaces.OrdersInterfaces;
using BL.Services.CommonServices;
using DAL.Context;
using DAL.Interfaces.UnitOfWork;
using DAL.Models.IdentityModels;
using DAL.UOW;
using LoggerSevice.Interface;
using LoggerSevice.Logger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using Swashbuckle.AspNetCore.Swagger;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
{
    {securityScheme, new string[] { }},
};
                c.AddSecurityRequirement(securityRequirements);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            });
           
            services.AddHttpContextAccessor();


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
              services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ILinkService, LinkService>();
           


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

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Main API v1");
                
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
