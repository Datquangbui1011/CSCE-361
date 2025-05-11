using System;
using System.Linq;
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
    public class UserControllerRegisterTests
    {
        private DbContextOptions<AppDbContext> _dbOptions;
        private AppDbContext _context = null!;
        private UserAccessor _accessor = null!;
        private UserManager _manager = null!;
        private UserController _controller = null!;
        private PasswordHasher<User> _hasher = new();

        [TestInitialize]
        public void Init()
        {
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context    = new AppDbContext(_dbOptions);
            _accessor   = new UserAccessor(_context);
            _manager    = new UserManager(_accessor);
            _controller = new UserController(_manager);
        }

        [TestMethod]
        public async Task Register_ReturnsBadRequest_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var dto = new RegisterDto {
                Name            = "Alice",
                Email           = "alice@x.com",
                Password        = "pwd1",
                ConfirmPassword = "pwd2"
            };

            // Act
            IActionResult result = await _controller.Register(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var bad = (BadRequestObjectResult)result;
            Assert.AreEqual("Passwords do not match.", bad.Value);
        }

        [TestMethod]
        public async Task Register_ReturnsConflict_WhenEmailAlreadyExists()
        {
            // Arrange: seed existing user
            var existing = new User {
                UserId   = Guid.NewGuid(),
                Name     = "Bob",
                Email    = "bob@x.com",
                Password = _hasher.HashPassword(null, "secret"),
                Address  = "Addr"
            };
            _context.Users.Add(existing);
            await _context.SaveChangesAsync();

            var dto = new RegisterDto {
                Name            = "Bob2",
                Email           = "bob@x.com",
                Password        = "newpass",
                ConfirmPassword = "newpass"
            };

            // Act
            IActionResult result = await _controller.Register(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            var conflict = (ConflictObjectResult)result;
            Assert.AreEqual("Email is already in use.", conflict.Value);
        }

        [TestMethod]
        public async Task Register_ReturnsOkAndPersistsUser()
        {
            // Arrange
            var dto = new RegisterDto {
                Name            = "Carol",
                Email           = "carol@x.com",
                Password        = "mypwd",
                ConfirmPassword = "mypwd"
            };

            // Act
            IActionResult result = await _controller.Register(dto);

            // Assert: response type
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;

            // persisted user?
            var user = _context.Users.SingleOrDefault(u => u.Email == dto.Email);
            Assert.IsNotNull(user, "User should be persisted");
            Assert.AreEqual(dto.Name, user!.Name);
            Assert.AreEqual(dto.Email, user.Email);
            // ensure password was hashed
            Assert.AreNotEqual(dto.Password, user.Password);
            var verify = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, dto.Password);
            Assert.AreEqual(PasswordVerificationResult.Success, verify);
        }
    }
}
