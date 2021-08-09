using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncProject.Data;
using AsyncProject.Models;
using AsyncProject.Models.Interfaces;
using AsyncProject.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AsyncProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _amenity;

        public AmenitiesController(IAmenity a)
        {
            _amenity = a;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
            // Count the list
            var list = await _amenity.GetAmenities();
            return Ok(list);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenity(int id)
        {
            // Awaiting a response from the service (service handles the extracting of the data from AsyncInn)
            AmenityDTO amenity = await _amenity.GetAmenity(id);

            return amenity;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "updateAmenity")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }
            var updatedAmenity = await _amenity.UpdateAmenity(id, amenity);
            return Ok(updatedAmenity);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "createAmenity")]
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenity(NewAmenityDTO amenity)
        {
            AmenityDTO newAmenity = await _amenity.Create(amenity);

            return CreatedAtAction("PostAmenity", new { id = newAmenity.ID } , newAmenity);
        }

        [Authorize(Policy = "deleteAmenity")]
        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _amenity.Delete(id);
            return NoContent();
        }
    }
}
