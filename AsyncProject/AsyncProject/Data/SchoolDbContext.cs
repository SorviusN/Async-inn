using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncProject.Models;

namespace AsyncProject.Data
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public SchoolDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This call the base method, but does nothing.
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Aman", LastName = "Almeida" },
                new Student { Id = 2, FirstName = "Hannah", LastName = "Larson" },
                new Student { Id = 3, FirstName = "Melanie", LastName = "Campbell" }
                );
        }
    }
}
