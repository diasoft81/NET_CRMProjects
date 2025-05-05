using CRM.Repositories.Entities.Generals;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Repositories.ContextExtension
{
    public static class ConfigBuilder
    {
        public static void BuildConfig(this ModelBuilder modelBuilder)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(SeederResource.Users);
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
