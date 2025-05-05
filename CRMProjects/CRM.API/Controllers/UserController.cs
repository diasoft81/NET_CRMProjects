using Microsoft.AspNetCore.Mvc;
using CRM.Repositories.Entities.Generals;
using CRM.Repositories.Interfaces;
using CRM.Service.Interface.General;
using Microsoft.Extensions.Caching.Memory;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserController(IMemoryCache cache, ILogger<UserController> logger,
                      IUserRepository userRepository,
                      IUserService userService)
        {
            _cache = cache;
            _logger = logger;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            if (!_cache.TryGetValue("AllUsers", out List<User> users))
            {
                users = await _userRepository.GetAllAsync();
                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set("AllUsers", users, options);
                _logger.LogInformation("Fetched users from database");
            }
            else
            {
                _logger.LogInformation("Loaded users from cache");
            }

            return users;
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // GET: api/User/search?keyword=john
        [HttpGet("search")]
        public async Task<ActionResult<List<User>>> LoadBy(string keyword)
        {
            var results = await _userRepository.SearchByKeywordAsync(keyword);
            return Ok(results);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, User user)
        {
            if (id != user.Id) return BadRequest("ID mismatch");
            var updated = await _userRepository.UpdateAsync(user);
            return Ok(updated);
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userRepository.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost("with-role")]
        public async Task<ActionResult<User>> CreateWithRole(User user, [FromQuery] int roleId)
        {
            var created = await _userService.CreateUserWithRoleAsync(user, roleId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}
