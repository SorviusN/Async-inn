using AsyncProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models.Interfaces
{
    public interface IAmenity 
    {
        // CREATE
        Task<AmenityDTO> Create(NewAmenityDTO amenity);

        // GET ALL
        Task<List<AmenityDTO>> GetAmenities();

        // GET 1 BY ID
        Task<AmenityDTO> GetAmenity(int id);

        // UPDATE
        Task<Amenity> UpdateAmenity(int id, Amenity amenity);

        // DELETE
        Task Delete(int id);
    }
}
