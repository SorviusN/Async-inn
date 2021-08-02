using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Studio is 0, One Bedroom is 1, Two bedroom is 2 for the layout number
        public int Layout { get; set; }
        public List<HotelRoom> HotelRooms { get; set; }
        public List<Amenity> RoomAmenities { get; set; }
    }
}
