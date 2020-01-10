using DAL.Context;
using DAL.Interfaces.UnitOfWork;
using DAL.Models.IdentityModels;
using DAL.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Configuration.Injection
{
   public class InfrastructureConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<IdentityContext>();
            
            
        }
        public static void DbContext(IServiceCollection services,string connection,string id_connection)
        {
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connection)
            ); ;
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(id_connection));
        }
    }
}
