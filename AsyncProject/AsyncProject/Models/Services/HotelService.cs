using AsyncProject.Data;
using AsyncProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AsyncProject.Models.Services
{
    public class HotelService : IHotel
    {
        private AsyncInnDbContext _context;

        // Below is dependency injection.
        public HotelService(AsyncInnDbContext context)
        {
            // Constructor that 
            _context = context;
        }
        async public Task<Hotel> Create(Hotel hotel)
        {
            // hotel is an instance of hotel 
            // the current state of hotel object: raw

            _context.Entry(hotel).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> GetHotel(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            return hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return hotels;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
        // returning "Task" means that we will, in essence, return nothing (task is for async)
        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
