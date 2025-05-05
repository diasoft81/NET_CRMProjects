using CRM.Repositories.Implementations;
using CRM.Repositories.Interfaces;
using CRM.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CRM.Service.Implementation.General.CRM.API.Services;
using CRM.Service.Interface.General;

namespace CRM.Service
{
    public static class ServiceContainer
    {
        public static void ServiceInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
