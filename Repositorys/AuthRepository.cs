using System.Linq;
using System.Threading.Tasks;
using HotelApp.Api.Dtos;
using HotelApp.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Api.Repositorys
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;

        public AuthRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var result =  await _userManager.CheckPasswordAsync(user,password);
            if(!result)
                return null;
            return user;
        }

        public async Task<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = new User ();
            user.UserName = userForRegisterDto.UserName;
            user.Email = userForRegisterDto.Email;
            user.PhoneNumber = userForRegisterDto.PhoneNumber;
            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);
            if(!result.Succeeded)
                return null;
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _userManager.Users.AnyAsync(x => x.UserName == username))
               return true;

            return false;
        }
    }
}