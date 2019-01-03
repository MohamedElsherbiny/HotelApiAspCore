using System.ComponentModel.DataAnnotations;

namespace HotelApp.Api.Dtos
{
    public class UserForEditDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}