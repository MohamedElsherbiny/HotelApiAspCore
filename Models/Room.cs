using System.Collections.Generic;

namespace HotelApp.Api.Models
{
    public enum RoomType
    {
        Adult,
        Children
    }
    public class Room
    {
        public int RoomId { get; set; }
        public int Rate { get; set; }
        public bool IsAvailable { get; set; }
        public RoomType RoomType { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}