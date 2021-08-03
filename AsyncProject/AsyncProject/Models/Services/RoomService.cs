﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncProject.Data;
using AsyncProject.Models.DTO;
using AsyncProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AsyncProject.Models.Services
{
    public class RoomService : IRoom
    {
        private AsyncInnDbContext _context;
        private IHotel _hotels;

        // Below is dependency injection. We need context and hotelService.
        public RoomService(AsyncInnDbContext context, IHotel hotelService)
        {
            // Constructor that gives a reference to the Database area "context"
            _context = context;
            _hotels = hotelService;
        }

        async public Task<Room> Create(Room room)
        {
            // hotel is an instance of hotel 
            // the current state of hotel object: raw

            _context.Entry(room).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<RoomDTO> GetRoom(int id)
        {
            // LINQ - returns a specific hotel attached to the room.
            // Return a room dto instead of room.
            // return await _context.Rooms
            // .Include(r => r.HotelRooms)
            // .ThenInclude(hr => hr.Hotel)
            // .FirstOrDefaultAsync(h => h.Id == id);

            // Casting
            return await _context.Rooms
                .Select(room => new RoomDTO
                {
                    ID = room.Id,
                    Name = room.Name,
                    Layout = room.Layout,
                    Amenities = room.RoomAmenities
                    .Select(t => new AmenityDTO
                    {
                        Name = t.Name
                    }).ToList()
                }).FirstOrDefaultAsync(r => r.ID == id);
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
            // find a room by its specific ID.
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
