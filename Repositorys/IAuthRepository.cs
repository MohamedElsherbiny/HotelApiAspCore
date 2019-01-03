using System.Threading.Tasks;
using HotelApp.Api.Dtos;
using HotelApp.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelApp.Api.Repositorys
{
    public interface IAuthRepository
    {
        Task<User> Register(UserForRegisterDto user);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}