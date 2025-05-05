using CRM.Repositories.ContextExtension;
using CRM.Repositories.Entities.Generals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Repositories
{
    public class AppDbContext : DbContext
    {
        public static string USER_FOLDER = @"D:\Test\DB\";
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
         : base(contextOptions)
        { }



        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildConfig();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = $@"Data Source={USER_FOLDER}/NETCRM.db";
                optionsBuilder.UseSqlite(connectionString);
                //optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}
