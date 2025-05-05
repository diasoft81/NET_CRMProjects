using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Repositories.Entities.Generals
{
    [Serializable]
    [Index(nameof(RoleName))]
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public long Rights { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
