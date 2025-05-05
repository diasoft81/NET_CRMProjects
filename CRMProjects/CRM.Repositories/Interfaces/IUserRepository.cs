using CRM.Repositories.Entities.Generals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<List<User>> SearchByKeywordAsync(string keyword);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}
