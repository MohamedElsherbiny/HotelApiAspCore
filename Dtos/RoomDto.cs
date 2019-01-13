using System.Collections.Generic;
using HotelApp.Api.Models;

namespace HotelApp.Api.Dtos
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public int Rate { get; set; }
        public bool IsAvailable { get; set; }
        public RoomType RoomType { get; set; }
        public int HotelId { get; set; }
    }
}