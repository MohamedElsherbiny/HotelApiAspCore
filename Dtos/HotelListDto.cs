using System.Collections.Generic;
using HotelApp.Api.Models;

namespace HotelApp.Api.Dtos
{
    public class HotelListDto
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
        public ICollection<RoomDto> Rooms { get; set; }
        public ICollection<ReservationDto> Reservations { get; set; }

    }
}