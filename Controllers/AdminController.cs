using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApp.Api.Dtos;
using HotelApp.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserValidator<User> _userValidator;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IUserValidator<User> userValidator,
            IPasswordValidator<User> passwordValidator,
            IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
        }
        // ========= Users ==============
        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users.Count > 0)
            {
                var usersToReturn = _mapper.Map<List<UserForListDto>>(users);
                return Ok(usersToReturn);
            }
            return NotFound("No Users");
        }
       [HttpGet("Users/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute]string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                var userToReturn = _mapper.Map<UserForListDto>(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                userToReturn.Roles = userRoles.ToArray();
                return Ok(userToReturn);
            }
            return NotFound("No User");
        }
        [HttpPost("Users/Add")]
        public async Task<IActionResult> CreateUser([FromBody]UserForCreateDto model)
        {
            if (!ModelState.IsValid)
                return StatusCode(401, ModelState);

            User user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            IdentityResult result
            = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,"users");
                return Ok();
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return StatusCode(401, ModelState);
        }
        [HttpPost("Users/Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User Not Found");

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return StatusCode(401, ModelState);
        }
        [HttpPost("Users/edit")]
        public async Task<IActionResult> Edit([FromBody]UserForEditDto userForEdit)
        {
            User user = await _userManager.FindByIdAsync(userForEdit.Id);
            if (user != null)
            {
                user.UserName = userForEdit.UserName;
                user.Email = userForEdit.Email;
                user.PhoneNumber = userForEdit.PhoneNumber;
                IdentityResult validEmail
                = await _userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    foreach (IdentityError error in validEmail.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(userForEdit.Password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager,
                    user, userForEdit.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user,
                        userForEdit.Password);
                    }
                    else
                    {
                        foreach (IdentityError error in validPass.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                if ((validEmail.Succeeded && validPass == null)
                || (validEmail.Succeeded
               && userForEdit.Password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return StatusCode((int)System.Net.HttpStatusCode.NotAcceptable, user);
        }

       // =========== Roles ==========
        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles.Count > 0)
            {
                return Ok(roles);
            }
            return NotFound("No Roles");
        }
        [HttpGet("Roles/{roleId}")]
        public async Task<IActionResult> GetRole([FromRoute]string roleId)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            if (role != null)
            {
                return Ok(role);
            }
            return NotFound("No Roles");
        }

        [HttpPost("Roles/Add/{name}")]
        public async Task<IActionResult> CreateRole([FromRoute]string name)
        {
            if (!ModelState.IsValid)
                return StatusCode((int)System.Net.HttpStatusCode.NotAcceptable, new {hello="hello"+ name});
            name = name.ToLower();

            IdentityRole roleToCreate = new IdentityRole(name);
            IdentityResult result = await _roleManager.CreateAsync(roleToCreate);
            if (!result.Succeeded)
                 return StatusCode((int)System.Net.HttpStatusCode.NotAcceptable); 

            return Ok();
        }
        [HttpPost("Roles/Delete/{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute]string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound("User Not Found");

            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return StatusCode(401, ModelState);
        }
        [HttpPost("Roles/Edit")]
        public async Task<IActionResult> EditRole([FromBody]RoleForEditDto roleForEdit)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(roleForEdit.Id);
            if (role == null)
                return Unauthorized();

            role.Name = roleForEdit.Name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            if(result.Succeeded)
                return Ok();
            return Unauthorized();
        }
        [HttpPost("Roles/AddToRole/{userId}/{role}")]
        public async Task<IActionResult> AddToRole([FromRoute]string userId,[FromRoute] string role)
        {

            IdentityRole roleFromRepo = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == role);
            User userFromRepo = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (roleFromRepo == null || userFromRepo == null)
                return Unauthorized();
            IdentityResult result = await _userManager.AddToRoleAsync(userFromRepo,roleFromRepo.Name);
            return Ok();
            
        }
        [HttpPost("Roles/IsInRole/{userId}/{role}")]
        public async Task<bool> IsInRole([FromRoute]string userId,[FromRoute] string role)
        {

            IdentityRole roleFromRepo = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == role);
            User userFromRepo = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (roleFromRepo == null || userFromRepo == null)
                return false;
            bool result = await _userManager.IsInRoleAsync(userFromRepo,roleFromRepo.Name);
            return result;
            
        }
        [HttpPost("Roles/RemoveFromRole/{userId}/{role}")]
        public async Task<ActionResult> RemoveFormRole([FromRoute]string userId,[FromRoute] string role)
        {

            IdentityRole roleFromRepo = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == role);
            User userFromRepo = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (roleFromRepo == null || userFromRepo == null)
                return Unauthorized();
            IdentityResult result = await _userManager.RemoveFromRoleAsync(userFromRepo,roleFromRepo.Name);
            if(!result.Succeeded)
                return  Unauthorized();
            return Ok();
            
        }
        [HttpPost("Roles/UsersInRole/{roleId}")]
        public async Task<ActionResult> UsersInRole([FromRoute] string roleId)
        {

            IdentityRole roleFromRepo = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            if (roleFromRepo == null)
                return Unauthorized();
            var result = await _userManager.GetUsersInRoleAsync(roleFromRepo.Name);
            List<User> users = result.ToList();
            var usersToReturn = _mapper.Map<List<UserForListDto>>(users);
            return Ok(usersToReturn);
        }
        
    }
}