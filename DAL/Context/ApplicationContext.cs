using DAL.Models.CommonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Type = DAL.Models.CommonModels.Type;

namespace DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<File> Files { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Path> Paths { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Models.CommonModels.Type> Types { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           



            modelBuilder.Entity<File>()
                 .HasOne(x => x.Path)
                 .WithOne(g => g.File)
                 .HasForeignKey<Path>(p => p.FileId)
                 .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<File>()
                .HasOne(x => x.Link)
                .WithOne(g => g.File)
                .HasForeignKey<Link>(p => p.FileId)
                .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<File>()
                .HasOne(x => x.Status)
                .WithMany(g => g.Files)
                .OnDelete(DeleteBehavior.SetNull);

           

            modelBuilder.Entity<File>()
                   .HasOne(x => x.Type)
                   .WithMany(g => g.Files)
                   .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Files)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<User>()
                .HasKey(x => x.IdenityId);

         

            modelBuilder.Entity<User>()
                .Property(x => x.IdenityId)
                .ValueGeneratedNever();

           

            modelBuilder.Entity<File>().Property(x => x.Name).HasMaxLength(30);
            modelBuilder.Entity<File>().Property(x => x.Description).HasMaxLength(100);
           



            modelBuilder.Entity<Status>().HasIndex(x => x.Title).IsUnique();
            modelBuilder.Entity<Type>().HasIndex(x => x.Format).IsUnique();
        }



        }
}
