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

        public DbSet<UserDevice>? UserDevices { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Apartment>? Apartments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Login).IsUnique();
            });
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Phone).IsUnique();
            });
            builder.Entity<UserDevice>(entity =>
            {
                entity.HasIndex(e => e.DeviceId).IsUnique();
            });
        }
    }
}

