using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.General
{
    public class Role
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public long Rights { get; set; }
    }
}
