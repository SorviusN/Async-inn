using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncProject.Data;
using AsyncProject.Models.DTO;
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
        async public Task<AmenityDTO> Create(NewAmenityDTO inboundAmenity)
        {
            // hotel is an instance of hotel 
            // the current state of hotel object: raw

            Amenity amenity = new Amenity()
            {
                Name = inboundAmenity.Name
            };

            _context.Entry(amenity).State = EntityState.Added;

            AmenityDTO addedAmenity = new AmenityDTO()
            {
                Name = amenity.Name
            };

            await _context.SaveChangesAsync();
            return addedAmenity;
        }

        public async Task<AmenityDTO> GetAmenity(int id)
        {
            return await _context.Amenities
                .Select(amenity => new AmenityDTO
                {
                    ID = amenity.Id,
                    Name = amenity.Name
                }).FirstOrDefaultAsync();
        }


        public async Task<List<AmenityDTO>> GetAmenities()
        {
            return await _context.Amenities
                .Select(amenity => new AmenityDTO
                {
                    ID = amenity.Id,
                    Name = amenity.Name
                }).ToListAsync();
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
            Amenity amenity = await _context.Amenities.FindAsync(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            // save the changes.
            await _context.SaveChangesAsync();
        }
    }
}
