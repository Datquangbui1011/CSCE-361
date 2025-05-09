using System;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;
using e_commerce_store_service_host.Server.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_store_service_host.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

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
            var normalizedEmail = login.Email.ToLower();
            var user = await _userManager.GetUserByEmailAsync(normalizedEmail);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Invalid email or password");

            return Ok(new
            {
                userId = user.UserId,
                name = user.Name,
                email = user.Email,
                address = user.Address
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            if (register.Password != register.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var normalizedEmail = register.Email.ToLower();
            var existingUser = await _userManager.GetUserByEmailAsync(normalizedEmail);
            if (existingUser != null)
            {
                return Conflict("Email is already in use.");
            }

            var user = new User
            {
                Email = normalizedEmail,
                Name = register.Name,
                Password = _passwordHasher.HashPassword(null, register.Password)
            };

            var result = await _userManager.AddUserAsync(user);

            if (!result)
            {
                return StatusCode(500, "An error occurred while registering the user.");
            }

            return Ok(new
            {
                name = user.Name,
                email = user.Email
            });
        }
    }
}
