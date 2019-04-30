using System;
using System.Collections.Generic;
using CarnGo.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CarnGo.Database
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mowinckel.database.windows.net,1433;Initial Catalog = CarnGo; Persist Security Info=False;User ID = ProjectDB@mowinckel;Password=Vores1.sødedatabase;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30");

        } 
        private DbSet<CarEquipment> CarEquipment { get; set; }
        private DbSet<User> Users { get; set; }
        private DbSet<Message> Messages { get; set; }
        private DbSet<CarProfile> CarProfiles { get; set; }
        private DbSet<DayThatIsRented> DaysThatIsRented { get; set; }
        private DbSet<PossibleToRentDay> PossibleToRentDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PossibleToRentDay>()
                .HasOne(p => p.CarProfile)
                .WithMany(b => b.PossibleToRentDays);

            modelBuilder.Entity<DayThatIsRented>()
                .HasOne(p => p.CarProfile)
                .WithMany(b => b.DaysThatIsRented);

            modelBuilder.Entity<CarProfile>()
                .HasOne(p => p.CarEquipment)
                .WithOne(b => b.CarProfile);

            modelBuilder.Entity<CarProfile>()
                .HasOne(p => p.User)
                .WithMany(b => b.Cars);

            modelBuilder.Entity<User>()
                .HasMany(p => p.MessagesWithUsers)
                .WithOne(b => b.User);

            modelBuilder.Entity<Message>()
                .HasMany(p => p.MessagesWithUsers)
                .WithOne(b => b.Message);

        }
    }
}
    