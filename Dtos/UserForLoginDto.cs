using System.ComponentModel.DataAnnotations;

namespace HotelApp.Api.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }        
    }
}