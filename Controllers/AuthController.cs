using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HotelApp.Api.Dtos;
using HotelApp.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;
using HotelApp.Api.Repositorys;

namespace HotelApp.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthRepository repo, IConfiguration configuration, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto user)
        {

            user.UserName = user.UserName.ToLower();
            System.Console.WriteLine(user);
            if (!ModelState.IsValid)
                return BadRequest();

            if (await _repo.UserExists(user.UserName))
                ModelState.AddModelError("Username", "Username already exist");


            var createUser = await _repo.Register(user);
            if(createUser == null)
                return BadRequest();
            return Ok();
            
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {
            userForLoginDto.Username = userForLoginDto.Username.ToLower();

            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return await GenerateToken(user);


        }
        private async Task<IActionResult> GenerateToken(User user)
        
        {
            var rolesFormRepo = await _userManager.GetRolesAsync(user);
            var rolesToReturn = rolesFormRepo.ToList();
            //generate the token
            string secretkey = _configuration.GetSection("secretkey").ToString();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            var ClaimsToAdd = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            foreach (var item in rolesToReturn)
            {
                ClaimsToAdd.Add(new Claim(ClaimTypes.Role, item));
            }
            var tokenDescroiptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( claims: ClaimsToAdd),
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                           SecurityAlgorithms.HmacSha384Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescroiptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }

        [HttpGet("crentUser")]
        public IActionResult crentUser() 
        {
            return Ok(User.IsInRole("admin"));

        }
    }

}