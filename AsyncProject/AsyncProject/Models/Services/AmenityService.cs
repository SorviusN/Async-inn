using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncProject.Data;
using AsyncProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace AsyncProject.Models.Services
{
    public class AmenityService : IAmenity 
    {
        private AsyncInnDbContext _context;

        // Below is dependency injection.
        public AmenityService(AsyncInnDbContext context)
        {
            // Constructor that gives a reference to the Database area "context"
            _context = context;
        }
        async public Task<Amenity> Create(Amenity amenity)
        {
            // hotel is an instance of hotel 
            // the current state of hotel object: raw

            _context.Entry(amenity).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task<Amenity> GetAmenity(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            return amenity;
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            var amenities = await _context.Amenities.ToListAsync();
            return amenities;
        }

        public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
        // returning "Task" means that we will, in essence, return nothing (task is for async)

        public async Task Delete(int id)
        {
            Amenity amenity = await GetAmenity(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
