using CRM.Repositories.Entities.Generals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
        Task<List<Role>> SearchByKeywordAsync(string keyword);
        Task<Role> AddAsync(Role role);
        Task<Role> UpdateAsync(Role role);
        Task<bool> DeleteAsync(int id);
    }
}
