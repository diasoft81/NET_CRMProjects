using CRM.Repositories.Entities.Generals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<List<UserRole>> GetAllAsync();
        Task<UserRole> GetByIdAsync(int id);
        Task<UserRole> AddAsync(UserRole userRole);
        Task<UserRole> UpdateAsync(UserRole userRole);
        Task<bool> DeleteAsync(int id);
    }
}
