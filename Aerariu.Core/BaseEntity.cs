using System.ComponentModel.DataAnnotations;

namespace Aerariu.Core
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
