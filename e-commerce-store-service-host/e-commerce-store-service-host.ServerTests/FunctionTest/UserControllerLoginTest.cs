using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Model.DTO;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Services;
using e_commerce_store_service_host.Server.Controllers;

namespace e_commerce_store_service_host.ServerTests.Controllers
{
    [TestClass]
    public class UserControllerLoginTests
    {
        private DbContextOptions<AppDbContext> _dbOptions;
        private AppDbContext      _context  = null!;
        private UserAccessor      _accessor = null!;
        private UserManager       _manager = null!;
        private UserController    _controller = null!;
        private PasswordHasher<User> _hasher = new();

        [TestInitialize]
        public void Init()
        {
            // fresh in-memory database each test
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context    = new AppDbContext(_dbOptions);
            _accessor   = new UserAccessor(_context);
            _manager    = new UserManager(_accessor);
            _controller = new UserController(_manager);
        }

        [TestMethod]
        public async Task Login_ReturnsUnauthorized_WhenUserNotFound()
        {
            // Act
            IActionResult result = await _controller.Login(
                new LoginDto { Email = "missing@x.com", Password = "any" });

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
            var unauthorized = (UnauthorizedObjectResult)result;
            Assert.AreEqual("Invalid email or password", unauthorized.Value);
        }

        [TestMethod]
        public async Task Login_ReturnsUnauthorized_WhenPasswordInvalid()
        {
            // ARRANGE: seed a user with a known password hash
            var user = new User {
                UserId   = Guid.NewGuid(),
                Name     = "TestUser",
                Email    = "user@x.com",
                Password = _hasher.HashPassword(null, "correctpw"),
                Address  = "Addr"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act: attempt login with wrong password
            IActionResult result = await _controller.Login(
                new LoginDto { Email = "user@x.com", Password = "wrongpw" });

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
            var unauthorized = (UnauthorizedObjectResult)result;
            Assert.AreEqual("Invalid email or password", unauthorized.Value);
        }

        [TestMethod]
        public async Task Login_ReturnsOk_WithCorrectPayload()
        {
            // ARRANGE: seed a user with a known password hash
            var plain = "letmein";
            var user = new User {
                UserId   = Guid.NewGuid(),
                Name     = "Carol",
                Email    = "carol@x.com",
                Password = _hasher.HashPassword(null, plain),
                Address  = "Wonderland"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            IActionResult result = await _controller.Login(
                new LoginDto { Email = "carol@x.com", Password = plain });

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;

            // pull out the anonymous payload via reflection
            var payload = ok.Value!;
            var t       = payload.GetType();
            var userId  = (Guid)   t.GetProperty("userId")!.GetValue(payload)!;
            var name    = (string) t.GetProperty("name")!.GetValue(payload)!;
            var email   = (string) t.GetProperty("email")!.GetValue(payload)!;
            var address = (string) t.GetProperty("address")!.GetValue(payload)!;

            Assert.AreEqual(user.UserId,  userId);
            Assert.AreEqual(user.Name,    name);
            Assert.AreEqual(user.Email,   email);
            Assert.AreEqual(user.Address, address);
        }
    }
}
