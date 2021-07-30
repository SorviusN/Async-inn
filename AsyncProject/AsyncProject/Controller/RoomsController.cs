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

namespace AsyncProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            // Count the list
            var list = await _room.GetRooms();
            return Ok(list);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            // Awaiting a response from the service (service handles the extracting of the data from AsyncInn)
            Room room = await _room.GetRoom(id);
            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }
            var updatedRoom = await _room.UpdateRoom(id, room);
            return Ok(updatedRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            await _room.Create(room);

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _room.Delete(id);
            return NoContent();
        }
    }
}
