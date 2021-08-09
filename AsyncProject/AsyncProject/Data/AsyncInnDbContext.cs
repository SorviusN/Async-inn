using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AsyncProject.Data
{
    public class AsyncInnDbContext : IdentityDbContext<ApplicationUser> // Old way: DbContext
    {
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        //Nav Property
        public DbSet<HotelRoom> HotelRooms { get; set; } 
        public DbSet<RoomAmenity> RoomAmenities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This call the base method, but does nothing.
            base.OnModelCreating(modelBuilder); // Similar to super from REACT

            // Seeding data for SQL
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
            },
            new Hotel
            {
                Id = 2,
                Name = "Async Inn Plus",
                StreetAddress = "10 Allston Street",
                City = "Boston",
                State = "Massachusetts",
                Country = "USA",
                Phone = "215-850-7772"
            }
            );
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Name = "Suite 1",
                    Layout = 2
                },
                new Room
                {
                    Id = 2,
                    Name = "Suite 2",
                    Layout = 1
                }
            );
            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Name = "TV",
                },
                new Amenity
                {
                    Id = 2,
                    Name = "Sofa"
                }
            );

            // Tacks anything after the first two parameters into a string array via the params type in
            // parameter declarations of SeedRole function
            SeedRole(modelBuilder, "District Manager", "createRoom", "createAmenity", "createHotel"
                                , "updateRoom", "udpateAmenity", "updateHotel"
                                , "deletRoom", "deleteAmenity", "deleteHotel"
                );

            SeedRole(modelBuilder, "Property Manager", "createRoom", "createAmenity",
                                    "updateRoom", "updateAmenity",
                                    "deleteAmenity");

            SeedRole(modelBuilder, "Agent", "createAmenity", "updateRoom", "updateAmenity", "deleteAmenity");

            modelBuilder.Entity<HotelRoom>().HasKey(
                // for every hotel room, create a new foreign key for both hotel and room.
                hotelRoom => new { hotelRoom.RoomId, hotelRoom.HotelId }
            );


            modelBuilder.Entity<RoomAmenity>().HasKey(
                // for every hotel room, create a new foreign key for both hotel and room.
                roomAmenity => new { roomAmenity.RoomId, roomAmenity.AmenityId }
            );
        }

        public void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permission)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            // Creating an entity of type Identity role that stores data.
            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaims = permission.Select(permission =>
                // assigning all of the values that we set up in Startup.cs
                new IdentityRoleClaim<string>
                {
                    Id = 1,
                    RoleId = role.Id,
                    ClaimType = "permissions",
                    ClaimValue = permission
                }
            ).ToArray();

            // Assigning the role claims to the role.
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }
}

