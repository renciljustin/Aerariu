using System.ComponentModel.DataAnnotations;

namespace Aerariu.API.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(maximumLength: 32, ErrorMessage = "Must be at least 4 to 32 characters.", MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 32, ErrorMessage = "Must be at least 8 to 32 characters.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Must be at least 2 to 64 characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public List<Guid> UserRoles { get; set; }
    }
}
