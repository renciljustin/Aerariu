using System.ComponentModel.DataAnnotations.Schema;

namespace Aerariu.Core.Models
{
    [Table("UserRoles")]
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
