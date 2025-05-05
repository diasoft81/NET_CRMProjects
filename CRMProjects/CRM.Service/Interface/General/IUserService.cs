using CRM.Repositories.Entities.Generals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Service.Interface.General
{
    public interface IUserService
    {
        Task<User> CreateUserWithRoleAsync(User user, int roleId);
    }
}
