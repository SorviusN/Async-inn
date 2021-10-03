using AsyncProject.Data;
using AsyncProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AsyncProject.Models.DTO;

namespace AsyncProject.Models.Services
{
    public class HotelService : IHotel
    {
        private AsyncInnDbContext _context;

        // Below is dependency injection.
        public HotelService(AsyncInnDbContext context)
        {
            // Constructor that injects the database for use in the service.
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

        public async Task<HotelDTO> GetHotel(int id)
        {
            //Mondo linq query

            //return await _context.Hotels
            //                    // First include just hotelrooms
            //                    .Include(s => s.HotelRooms)
            //                    // then every room in hotel rooms
            //                    .ThenInclude(e => e.Room)
            //                    // room which matches the correct ID
            //                    .FirstOrDefaultAsync(s => s.Id == id);
            return await _context.Hotels
                .Select(hotel => new HotelDTO
                {
                    ID = hotel.Id,
                    Name = hotel.Name,
                    City = hotel.City,
                    Rooms = hotel.HotelRooms
                    .Select(h => new HotelRoomDTO
                    {
                        Room = h.Room.Name
                    }).ToList()
                }).FirstOrDefaultAsync(r => r.ID == id);
        }
        public async Task<Hotel> GetHotelByName(string name)
            {
            return await _context.Hotels
                .Include(a => a.HotelRooms)
                .ThenInclude(b => b.Room)
                .FirstOrDefaultAsync(m => m.Name == name);
            }

        public async Task<List<HotelDTO>> GetHotels()
        {
            return await _context.Hotels
                .Select(hotel => new HotelDTO
                {
                    ID = hotel.Id,
                    Name = hotel.Name,
                    City = hotel.City,
                    Rooms = hotel.HotelRooms
                    .Select(h => new HotelRoomDTO
                    {
                        Room = h.Room.Name
                    }).ToList()
                }).ToListAsync();
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
            Hotel hotel = await _context.Hotels.FindAsync();
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task AddRoom(int roomId, int hotelId)
        {
            HotelRoom hotelRoom = new HotelRoom()
            {
                RoomId = roomId,
                HotelId = hotelId
            };

            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
    }
}
