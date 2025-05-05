using CRM.Repositories.Implementations;
using CRM.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CRM.Repositories
{
    public static class RepositoryContainer
    {
        public static void RepositoryInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(DbContext), typeof(AppDbContext));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }
    }
}
