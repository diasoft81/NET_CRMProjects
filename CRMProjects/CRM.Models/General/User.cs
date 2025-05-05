using System.ComponentModel.DataAnnotations;

namespace CRM.Models.General
{
    public class User
    {
        public int Id { get; set; } = 0;

        [StringLength(100)]
        public required string UserName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public User() { }
    }
}