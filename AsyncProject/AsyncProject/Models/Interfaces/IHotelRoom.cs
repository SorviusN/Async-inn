using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models.Interfaces
{
    interface IHotelRoom
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

