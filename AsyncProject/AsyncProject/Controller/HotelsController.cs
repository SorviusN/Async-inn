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
    public class HotelsController : ControllerBase
    {
        //_hotel is "hotel service" which uses the actual database content.
        private readonly IHotel _hotel;
        public HotelsController(IHotel h)
        {
            _hotel = h;
        }

        // GET: api/Hotels
        [HttpGet] //The "Get" is when we want to receive Hotels.
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            // Count the list.
            var list = await _hotel.GetHotels();
            return Ok(list);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            HotelDTO hotel = await _hotel.GetHotel(id);
            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "updateHotel")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            var updatedHotel = await _hotel.UpdateHotel(id, hotel);

            return Ok(updatedHotel);
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "createHotel")]
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            // Return 201 Header to browser.
            // Body of req will be us running GetHotel(id)

            // if we decided to post a hotel through postman, we would put the hotel info
            //inside of body.
            await _hotel.Create(hotel);

            // Creates a hotel and adds an ID to it.
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }


        [Authorize(Policy = "deleteHotel")]
        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotel.Delete(id);

            //equivalent of returning void.
            return NoContent();
        }
    }
}
