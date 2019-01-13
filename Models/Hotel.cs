using System.Collections.Generic;

namespace HotelApp.Api.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Street  { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Coordinates { get; set; }
        public int Stars { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<PointOfInterest> PointOfInterests { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}