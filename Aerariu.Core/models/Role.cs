using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerariu.Core.Models
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string RoleName { get; set; }
        [Required]
        [Range(1, 10)]
        [DefaultValue(RoleLevel.User)]
        public RoleLevel RoleLevel { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }

    public enum RoleLevel
    {
        Superuser = 0,
        Administrator = 1,
        User = 2
    }
}
