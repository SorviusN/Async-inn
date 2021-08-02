using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models
{
    // This class is a nav property which acts as a medium between connecting 
    // Hotels with rooms via their IDs.
    public class HotelRoom
    { 
        // Composite key 1
        public int HotelId { get; set; }
        // Composite key 2
        public int RoomId { get; set; }

        // Nav Properties
        // Specifies our linkages between the tables
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
    }
}
