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
        Task<List<Hotel>> GetHotels();

        // GET 1 BY ID
        Task<Hotel> GetHotel(int id);

        // UPDATE
        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        // DELETE
        Task Delete(int id);
    }
}
