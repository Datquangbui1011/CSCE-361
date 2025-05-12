using System;
using System.Linq;
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
    public class UserControllerInMemoryTests
    {
        private DbContextOptions<AppDbContext> _dbOptions;
        private AppDbContext      _context = null!;
        private UserAccessor      _accessor = null!;
        private UserManager       _manager = null!;
        private UserController    _controller = null!;
        private PasswordHasher<User> _hasher = new();

        [TestInitialize]
        public void Setup()
        {
            // fresh in-memory database each time
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context    = new AppDbContext(_dbOptions);
            _accessor   = new UserAccessor(_context);   // real accessor :contentReference[oaicite:8]{index=8}:contentReference[oaicite:9]{index=9}
            _manager    = new UserManager(_accessor);   // real manager  :contentReference[oaicite:10]{index=10}:contentReference[oaicite:11]{index=11}
            _controller = new UserController(_manager); // real controller :contentReference[oaicite:12]{index=12}:contentReference[oaicite:13]{index=13}
        }

        [TestMethod]
        public async Task GetUser_ReturnsOk_WhenExists()
        {
            // ARRANGE: full user
            var u = new User {
                UserId   = Guid.NewGuid(),
                Name     = "Alice",
                Email    = "alice@x.com",
                Password = _hasher.HashPassword(null, "pw"),
                Address  = "123 St"
            };
            _context.Users.Add(u);
            await _context.SaveChangesAsync();

            // ACT
            IActionResult result = await _controller.GetUser(u.UserId);

            // ASSERT
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;
            Assert.AreSame(u, ok.Value);
        }

        [TestMethod]
        public async Task GetUser_ReturnsNotFound_WhenMissing()
        {
            IActionResult result = await _controller.GetUser(Guid.NewGuid());
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateUser_ReturnsCreatedAtAction_AndPersists()
        {
            var toAdd = new User {
                UserId   = Guid.NewGuid(),
                Name     = "Bob",
                Email    = "bob@x.com",
                Password = _hasher.HashPassword(null, "pw"),
                Address  = "456 Ave"
            };

            IActionResult result = await _controller.CreateUser(toAdd);

            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var created = (CreatedAtActionResult)result;
            Assert.AreEqual(nameof(_controller.GetUser), created.ActionName);
            Assert.AreEqual(toAdd.UserId, created.RouteValues["id"]);

            // persisted?
            var persisted = await _context.Users.FindAsync(toAdd.UserId);
            Assert.IsNotNull(persisted);
            Assert.AreEqual("Bob", persisted!.Name);
        }

        [TestMethod]
        public async Task CreateUser_ReturnsBadRequest_WhenModelInvalid()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            IActionResult result = await _controller.CreateUser(new User());
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteUser_ReturnsNoContent_AndRemoves()
        {
            var keep   = new User {
                UserId   = Guid.NewGuid(),
                Name     = "Keep",
                Email    = "keep@x.com",
                Password = _hasher.HashPassword(null, "pw"),
                Address  = "A"
            };
            var remove = new User {
                UserId   = Guid.NewGuid(),
                Name     = "Remove",
                Email    = "rem@x.com",
                Password = _hasher.HashPassword(null, "pw"),
                Address  = "B"
            };
            _context.Users.AddRange(keep, remove);
            await _context.SaveChangesAsync();

            IActionResult result = await _controller.DeleteUser(remove.UserId);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var remaining = _context.Users.ToList();
            Assert.AreEqual(1, remaining.Count);
            Assert.AreEqual(keep.UserId, remaining[0].UserId);
        }

        [TestMethod]
        public async Task Login_ReturnsUnauthorized_WhenNotFoundOrBadPassword()
        {
            // (1) not found
            IActionResult res1 = await _controller.Login(new LoginDto { Email="x@x", Password="p" });
            Assert.IsInstanceOfType(res1, typeof(UnauthorizedObjectResult));

            // (2) wrong password — seed full user
            var u = new User {
                UserId   = Guid.NewGuid(),
                Name     = "User",
                Email    = "u@x.com",
                Password = _hasher.HashPassword(null, "right"),
                Address  = "Somewhere"
            };
            _context.Users.Add(u);
            await _context.SaveChangesAsync();

            IActionResult res2 = await _controller.Login(new LoginDto { Email="u@x.com", Password="wrong" });
            Assert.IsInstanceOfType(res2, typeof(UnauthorizedObjectResult));
        }

        [TestMethod]
        public async Task Login_ReturnsOk_WithCorrectPayload_WhenSuccessful()
        {
            // seed full user
            var plain = "secret";
            var u = new User {
                UserId   = Guid.NewGuid(),
                Name     = "Carol",
                Email    = "c@x.com",
                Password = _hasher.HashPassword(null, plain),
                Address  = "Addr"
            };
            _context.Users.Add(u);
            await _context.SaveChangesAsync();

            IActionResult result = await _controller.Login(new LoginDto { Email="c@x.com", Password=plain });

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;

            // reflect into the anonymous payload
            var payload    = ok.Value!;
            var t          = payload.GetType();
            var userId     = (Guid) t.GetProperty("userId")!.GetValue(payload)!;
            var name       = (string)t.GetProperty("name"  )!.GetValue(payload)!;
            var email      = (string)t.GetProperty("email" )!.GetValue(payload)!;
            var address    = (string)t.GetProperty("address")!.GetValue(payload)!;

            Assert.AreEqual(u.UserId, userId);
            Assert.AreEqual(u.Name,   name);
            Assert.AreEqual(u.Email,  email);
            Assert.AreEqual(u.Address,address);
        }

        [TestMethod]
        public async Task Register_Cases()
        {
            // (1) passwords mismatch
            IActionResult r1 = await _controller.Register(new RegisterDto {
                Email           = "e@x.com",
                Name            = "N",
                Password        = "a",
                ConfirmPassword = "b"
            });
            Assert.IsInstanceOfType(r1, typeof(BadRequestObjectResult));

            // (2) email conflict — seed full user
            var existing = new User {
                UserId   = Guid.NewGuid(),
                Name     = "Existing",
                Email    = "t@x.com",
                Password = _hasher.HashPassword(null,"p"),
                Address  = "Addr"
            };
            _context.Users.Add(existing);
            await _context.SaveChangesAsync();

            IActionResult r2 = await _controller.Register(new RegisterDto {
                Email           = "t@x.com",
                Name            = "N",
                Password        = "p",
                ConfirmPassword = "p"
            });
            Assert.IsInstanceOfType(r2, typeof(ConflictObjectResult));

            // (3) successful registration
            IActionResult r3 = await _controller.Register(new RegisterDto {
                Email           = "new@x.com",
                Name            = "NewName",
                Password        = "p",
                ConfirmPassword = "p"
            });
            Assert.IsInstanceOfType(r3, typeof(OkObjectResult));
            var ok3     = (OkObjectResult)r3;
            var body    = ok3.Value!;
            var bt      = body.GetType();
            var regName = (string)bt.GetProperty("name")!.GetValue(body)!;
            var regEmail= (string)bt.GetProperty("email")!.GetValue(body)!;

            Assert.AreEqual("NewName",  regName);
            Assert.AreEqual("new@x.com",regEmail);
        }
    }
}
