namespace HotelApp.Api.Dtos
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HotelId { get; set; }
    }
}