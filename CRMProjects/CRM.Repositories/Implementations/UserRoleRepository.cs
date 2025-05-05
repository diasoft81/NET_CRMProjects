using CRM.Repositories.Entities.Generals;
using CRM.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Repositories.Implementations
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<UserRole?> GetByIdAsync(int id)
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.Id == id);
        }

        public async Task<UserRole> AddAsync(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return userRole;
        }

        public async Task<UserRole> UpdateAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
            return userRole;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ur = await _context.UserRoles.FindAsync(id);
            if (ur == null) return false;
            _context.UserRoles.Remove(ur);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
