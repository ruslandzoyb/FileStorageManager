using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Context
{
   public class IdentityContext : IdentityDbContext<ApplicationUser>
    {

        public IdentityContext(DbContextOptions options) :base(options)
        {
            Database.EnsureCreated();
        }
       
        
        //public IdentityContext(DbContextOptions<IdentityContext> options)
        //  : base(options)
        //{
        //}

        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server = (localdb); Database = IdentityUsers; Trusted_Connection = True; MultipleActiveResultSets = True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
