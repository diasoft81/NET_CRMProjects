﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Repositories.Entities.Generals
{
    [Table("UserRoles")]
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
