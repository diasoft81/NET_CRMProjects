using CRM.Repositories;
using CRM.Repositories.Entities.Generals;
using CRM.Repositories.Implementations;
using CRM.Repositories.Interfaces;
using CRM.Service.Implementation.General.CRM.API.Services;
using CRM.Service.Interface.General;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CRM.Tests.Service
{

    namespace CRM.Tests
    {
        public class UserServiceTests
        {
            private AppDbContext _context;
            private IUserRepository _userRepo;
            private IUserRoleRepository _userRoleRepo;
            private IUserService _userService;

            [SetUp]
            public async Task Setup()
            {
                var connection = new SqliteConnection("Filename=:memory:");
                connection.Open(); // penting!

                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlite(connection)
                    .Options;

                //var options = new DbContextOptionsBuilder<AppDbContext>()
                //    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                //    .Options;

                _context = new AppDbContext(options);
                await _context.Database.EnsureCreatedAsync();

                _userRepo = new UserRepository(_context);
                _userRoleRepo = new UserRoleRepository(_context);
                _userService = new UserService(_context, _userRepo, _userRoleRepo);

                // Seed a role
                _context.Roles.Add(new Role { Id = 1, RoleName = "Admin", Rights = 1 });
                _context.SaveChanges();
            }

            [Test]
            public async Task CreateUserWithRoleAsync_Should_Create_User_And_Assign_Role()
            {
                var user = new User
                {
                    UserName = "jdoe",
                    FullName = "John Doe",
                    Email = "jdoe@example.com",
                    Phone = "1234567890",
                    Password = "secret"
                };

                var result = await _userService.CreateUserWithRoleAsync(user, 1);

                Assert.IsNotNull(result);
                Assert.AreNotEqual(0, result.Id);

                var savedUser = await _context.Users.FindAsync(result.Id);
                var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == result.Id && ur.RoleId == 1);

                Assert.IsNotNull(savedUser);
                Assert.IsNotNull(userRole);
            }

            [TearDown]
            public void Cleanup()
            {
                _context.Dispose();
            }
        }
    }

}
