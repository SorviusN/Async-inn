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
            Room room = await _context.Rooms.FindAsync(id);
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return rooms;
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
