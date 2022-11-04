using System;
using Microsoft.EntityFrameworkCore;
using _787DD97A_API.Models;

namespace _787DD97A_API.Classes
{
    public class ApplicationContext : DbContext
    {
        //private readonly IConfiguration _configuration;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<UserDevice>? UserDevice { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<Apartment>? Apartments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<UserN>(entity =>
            //{
            //    entity.HasIndex(e => e.Login).IsUnique();
            //});
            //builder.Entity<UserN>(entity =>
            //{
            //    entity.HasIndex(e => e.Phone).IsUnique();
            //});

            //builder.Entity<Group>(entity =>
            //{
            //    entity.HasIndex(e => e.GroupName).IsUnique();
            //});
            //builder.Entity<UserDevice>(entity =>
            //{
            //    entity.HasIndex(e => e.DeviceId).IsUnique();
            //});
        }
    }
}

