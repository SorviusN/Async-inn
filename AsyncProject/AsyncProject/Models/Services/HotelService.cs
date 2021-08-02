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
            //Hotel hotel = await _context.Hotels.FindAsync(id);
            //return hotel;

            //Hotel hotel = await _context.Hotels.FindAsync(id);
            //var hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == id)
            //                                        .Include(x => x.Room)
            //                                        .ToListAsync();
            //hotel.HotelRooms = hotelRooms;
            //return hotel;

            //Mondo linq query

            return await _context.Hotels
                                // First include just hotelrooms
                                .Include(s => s.HotelRooms)
                                // then every room in hotel rooms
                                .ThenInclude(e => e.Room)
                                // room which matches the correct ID
                                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Hotel>> GetHotels()
        {
            return await _context.Hotels
                                // specifically hotel rooms in hotel
                                // Then the room part of the nav property
                                // then put it to a list and return.
                                .Include(h => h.HotelRooms)
                                .ThenInclude(hr => hr.Room)
                                .ToListAsync();
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
