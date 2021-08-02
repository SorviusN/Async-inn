using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models
{
    public class RoomAmenity
    {
        public int AmenityId { get; set; }
        public int RoomId { get; set; }

        // Nav Properties
        // Specifies linkage between the tables
        public Amenity Amenity { get; set; }
        public Room Room { get; set; }
    }
}
