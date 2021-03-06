using AsyncProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models.Interfaces
{
    public interface IRoom
    {
        // CREATE
        Task<RoomDTO> Create(NewRoomDTO room);

        // GET ALL
        Task<List<RoomDTO>> GetRooms();

        // GET 1 BY ID
        Task<RoomDTO> GetRoom(int id);

        // UPDATE
        Task<Room> UpdateRoom(int id, Room room);

        Task AddAmenityToRoom(int roomId, int amenityId);

        Task RemoveAmenityFromRoom(int roomId, int amenityId);

        // DELETE
        Task Delete(int id);
    }
}
