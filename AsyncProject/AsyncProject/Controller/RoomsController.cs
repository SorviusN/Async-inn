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
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        // We need a constructor with IRoom type for this class for security.
        public RoomsController(IRoom r)
        {
            _room = r;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            // Currently returning list of rooms, but we want packaged up rooms.
            // Count the list
            var list = await _room.GetRooms();
            return Ok(list);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            // THIS IS WHERE WE WANT TO CHANGE TO ROOMDTO.
            // Awaiting a response from the service (service handles the extracting of the data from AsyncInn)
            RoomDTO room = await _room.GetRoom(id);
            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "updateRoom")]
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
        [Authorize(Policy = "createRoom")]
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(NewRoomDTO room)
        {
            RoomDTO newRoom = await _room.Create(room);

            return CreatedAtAction("PostRoom", new { id = newRoom.ID }, newRoom);
        }

        [Authorize(Policy = "updateRoom")]
        [HttpPost]
        [Route("{roomId}/{amenityId}")]
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _room.AddAmenityToRoom(roomId, amenityId);
            return NoContent(); 
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
