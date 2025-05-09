using System;
using System.Threading.Tasks;
using System.Reflection;
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
    public class UserControllerLoginCaseInsensitiveTests
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
        public async Task Login_IsCaseInsensitive_ForEmail()
        {
            // ARRANGE: seed a user with mixed-case email
            var plain = "CasePass123";
            var user = new User {
                UserId   = Guid.NewGuid(),
                Name     = "CaseTester",
                Email    = "Mixed.Case@Example.COM",  // stored mixed case
                Password = _hasher.HashPassword(null, plain),
                Address  = "Case St"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // ACT: login with lowercase email
            IActionResult result = await _controller.Login(new LoginDto {
                Email    = "mixed.case@example.com",  // all lowercase
                Password = plain
            });

            // ASSERT: should return Ok and preserve stored casing in payload
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;

            var payload = ok.Value!;
            var t       = payload.GetType();
            var returnedEmail = (string)t.GetProperty("email")!.GetValue(payload)!;

            Assert.AreEqual(user.Email, returnedEmail);
        }
    }
}