using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Room> RoomAmenities { get; set; }
    }
}
