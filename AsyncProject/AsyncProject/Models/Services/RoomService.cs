using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncProject.Data;
using AsyncProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AsyncProject.Models.Services
{
    public class RoomService : IRoom
    {
        private AsyncInnDbContext _context;

        // Below is dependency injection.
        public RoomService(AsyncInnDbContext context)
        {
            // Constructor that gives a reference to the Database area "context"
            _context = context;
        }
        async public Task<Room> Create(Room room)
        {
            // hotel is an instance of hotel 
            // the current state of hotel object: raw

            _context.Entry(room).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> GetRoom(int id)
        {
            // LINQ - returns a specific hotel attached to the room.
            return await _context.Rooms
                                .Include(r => r.HotelRooms)
                                .ThenInclude(hr => hr.Hotel)
                                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<Room>> GetRooms()
        {
        // include all hotels that are attached to this room 
        // Turn it into a list and return that list.
            return await _context.Rooms
                                .Include(r => r.HotelRooms)
                                .ThenInclude(hr => hr.Hotel)
                                .ToListAsync();
        }

        public async Task AddAmenityToRoom(int roomId,int amenityId)
        {
            RoomAmenity RoomAmenity = new RoomAmenity()
            {
                AmenityId = amenityId,
                RoomId = roomId
            };

            _context.Entry(RoomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            Room room = await GetRoom(roomId);
            List<Amenity> ra = room.RoomAmenities;
            for (int i = 0; i < ra.Count; i++)
            {
                if (ra[i].Id == amenityId)
                {
                    _context.Entry(ra[i]).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();
                    break;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
        // returning "Task" means that we will, in essence, return nothing (task is for async)

        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
