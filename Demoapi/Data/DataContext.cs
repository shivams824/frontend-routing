using Demoapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Demoapi.Authentication;
// using JWTAuthentication.Authentication;

namespace Demoapi.Data
{ 
    public class DataContext : IdentityDbContext<User>
    {
      public DataContext(DbContextOptions options) : base(options)
      {
        
      }

      public DbSet<User> Users { get; set; }
      public DbSet<Pump> Pumps { get; set; }
      public DbSet<Camera> Cameras { get; set; }
      public DbSet<Wheel> Wheels { get; set; }
      public DbSet<Admin> Admins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Pump>()
              .HasOne(u => u.User)
              .WithMany(p => p.Pumps)
              .HasForeignKey(u => u.UserId)
              .IsRequired();

            modelBuilder.Entity<User>()
              .HasMany(p => p.Pumps)
              .WithOne(u => u.User)
              .HasForeignKey(u => u.UserId)
              .IsRequired();

            modelBuilder.Entity<Wheel>()
              .HasOne(u => u.User)
              .WithMany(w => w.Wheels)
              .HasForeignKey(a => a.UserId)
              .IsRequired();

            modelBuilder.Entity<User>()
              .HasMany(w => w.Wheels)
              .WithOne(u => u.User)
              .HasForeignKey(u => u.UserId)
              .IsRequired();

            modelBuilder.Entity<Camera>()
              .HasOne(u => u.User)
              .WithMany(c => c.Cameras)
              .HasForeignKey(u => u.UserId)
              .IsRequired();

            modelBuilder.Entity<User>()
              .HasMany(c => c.Cameras)
              .WithOne(u => u.User)
              .HasForeignKey(u => u.UserId)
              .IsRequired();

               //DataContext.SaveChanges();
              // modelBuilder.Entity<Pump>().HasData(PumpsList);
              // modelBuilder.Entity<Pump>().HasData(CameraList);
              // modelBuilder.Entity<Pump>().HasData(WheelList);
              // modelBuilder.Entity<User>().HasData(UsersList);
    }
  } 
}