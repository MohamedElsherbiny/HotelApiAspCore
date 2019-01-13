namespace HotelApp.Api.Models
{
    public class PointOfInterest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        
    }
}