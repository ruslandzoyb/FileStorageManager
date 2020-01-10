using DAL.Context;
using DAL.Interfaces.UnitOfWork;
using DAL.Models.IdentityModels;
using DAL.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            services.AddDbContext<ApplicationContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<IdentityContext>();
            
        }
    }
}
