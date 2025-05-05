using CRM.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EziConfig.Blazor
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = $@"Data Source={AppDbContext.USER_FOLDER}/NETCRM.db";
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite($"Data Source={connectionString}", x => x.MigrationsAssembly("CRM.API"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
