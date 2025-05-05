using global::CRM.Repositories.Interfaces;
using global::CRM.Repositories;
using Microsoft.EntityFrameworkCore;
using CRM.Repositories.Entities.Generals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Service.Interface.General;
using CRM.Service.Interface.Message;
using System.Text.Json;

namespace CRM.Service.Implementation.General
{

    namespace CRM.API.Services
    {
        public class UserService : IUserService
        {
            private readonly AppDbContext _context;
            private readonly IUserRepository _userRepo;
            private readonly IUserRoleRepository _userRoleRepo;
            private readonly IRabbitMqService _rabbitMq;

            public UserService(AppDbContext context,
                IRabbitMqService rabbitMq,
                               IUserRepository userRepo,
                               IUserRoleRepository userRoleRepo)
            {
                _context = context;
                _rabbitMq = rabbitMq;
                _userRepo = userRepo;
                _userRoleRepo = userRoleRepo;
            }

            public async Task<User> CreateUserWithRoleAsync(User user, int roleId)
            {
                using var trx = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Step 1: Tambah user
                    var createdUser = await _userRepo.AddAsync(user);

                    // Step 2: Tambah user-role
                    var userRole = new UserRole
                    {
                        UserId = createdUser.Id,
                        RoleId = roleId
                    };

                    await _userRoleRepo.AddAsync(userRole);

                    await trx.CommitAsync();

                    _rabbitMq.Publish("user.created", JsonSerializer.Serialize(user));

                    return createdUser;
                }
                catch
                {
                    await trx.RollbackAsync();
                    throw;
                }
            }
        }
    }

}
