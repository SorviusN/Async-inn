using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncProject.Models;

namespace AsyncProject.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Hotel> Hotel { get; set; }

        public DbSet<Room> Room { get; set; }

        public DbSet<Amenity> Amenity { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This call the base method, but does nothing.
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Async Inn",
                    StreetAddress = "3211 West Boulevard",
                    City = "Cleveland",
                    State = "Ohio",
                    Country = "USA",
                    Phone = "859-225-0283"
                }
            );
        }
    }
}
