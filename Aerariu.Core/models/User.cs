using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Core.Models
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 32, ErrorMessage = "Must be at least 4 to 32 characters.", MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Must be at least 2 to 64 characters.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
