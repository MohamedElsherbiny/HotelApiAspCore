using System;

namespace HotelApp.Api.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string GustName { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string ConfirmKey { get; set; }
        public string EmployeeName { get; set; }
    }
}