using Microsoft.AspNetCore.Mvc;
using CRM.Repositories.Entities.Generals;
using CRM.Repositories.Interfaces;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/role
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAll()
        {
            var roles = await _roleRepository.GetAllAsync();
            return Ok(roles);
        }

        // GET: api/role/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        // GET: api/role/search?keyword=admin
        [HttpGet("search")]
        public async Task<ActionResult<List<Role>>> Search(string keyword)
        {
            var roles = await _roleRepository.SearchByKeywordAsync(keyword);
            return Ok(roles);
        }

        // POST: api/role
        [HttpPost]
        public async Task<ActionResult<Role>> Create(Role role)
        {
            var created = await _roleRepository.AddAsync(role);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/role/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> Update(int id, Role role)
        {
            if (id != role.Id) return BadRequest("ID mismatch");
            var updated = await _roleRepository.UpdateAsync(role);
            return Ok(updated);
        }

        // DELETE: api/role/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _roleRepository.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
