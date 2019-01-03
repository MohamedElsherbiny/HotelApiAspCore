using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HotelApp.Api.Models
{
    public class User : IdentityUser
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
    }
}