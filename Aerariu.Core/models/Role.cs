using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Core.models
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        [Required]
        public string RoleName { get; set; }
        [Required]
        [DefaultValue(RoleLevel.User)]
        public RoleLevel RoleLevel { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }

    public enum RoleLevel
    {
        Superuser = 0,
        Administrator = 1,
        Moderator = 2,
        User = 3
    }
}
