using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models.DTO
{

    //This is what we want room to look like in API results.
    public class RoomDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }
        public List<AmenityDTO> Amenities { get; set; }

    }
}
