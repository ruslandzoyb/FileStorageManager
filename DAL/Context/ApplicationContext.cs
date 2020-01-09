using DAL.Models.CommonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
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
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //    .AddJsonFile("dalconfig.json", optional: true, reloadOnChange: true);
            //var config = builder.Build();

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FilesDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //использование Fluent API

            //modelBuilder.Entity<Status>()
            //    .HasIndex(x => new { x.Id, x.Title });




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

            //modelBuilder.Entity<File>()
            //    .Property(x => x.User)
            //    .IsRequired();




            modelBuilder.Entity<User>()
                .HasKey(x => x.IdenityId);

            modelBuilder.Entity<User>()
                .Property(x => x.FullName)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.IdenityId)
                .ValueGeneratedNever();

            //modelBuilder.Entity<User>()
            //      .HasMany(x => x.Files)
            //      .WithOne(x => x.User);

            //modelBuilder.Entity<File>().Property(x => x.Link).IsRequired(false);
            //modelBuilder.Entity<File>().Property(x => x.Name).IsRequired(true);
            //modelBuilder.Entity<File>().Property(x => x.Type).IsRequired(true);
            //modelBuilder.Entity<File>().Property(x => x.Path).IsRequired(true);

            modelBuilder.Entity<File>().Property(x => x.Name).HasMaxLength(30);
            modelBuilder.Entity<File>().Property(x => x.Description).HasMaxLength(100);
            //modelBuilder.Entity<File>().Property(x => x.Status).HasDefaultValue(new Status()
            //{
            //    Title = "Private"
            //});

            //modelBuilder.Entity<File>().Property(x => x.Creation)
            //    .HasComputedColumnSql($"SELECT ADDDATE({DateTime.Now})");



            modelBuilder.Entity<Status>().HasIndex(x => x.Title).IsUnique();
        }



        }
}
