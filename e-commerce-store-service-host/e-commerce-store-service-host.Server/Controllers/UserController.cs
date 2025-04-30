using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace e_commerce_store_service_host.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userManager.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userManager.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _userManager.GetUserByEmailAsync(login.Email);
            if (user == null || user.Password != login.Password)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(new
            {
                userId = user.UserId,
                name = user.Name,
                email = user.Email,
                address = user.Address
            });
        }
    }
    
}