using AsyncProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models.Interfaces
{
    public interface IHotel
    {
        // CREATE
        Task<Hotel> Create(Hotel hotel);

        // GET ALL
        Task<List<HotelDTO>> GetHotels();

        // GET HOTEL BY ID
        Task<HotelDTO> GetHotel(int id);

        // GET HOTEL BY NAME INSTEAD
        Task<Hotel> GetHotelByName(string name);
        // UPDATE
        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        Task AddRoom(int roomId, int hotelId);

        // DELETE
        Task Delete(int id);
    }
}
