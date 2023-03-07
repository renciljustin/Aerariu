using System.ComponentModel.DataAnnotations;

namespace Aerariu.API.Dtos
{
    public class UserRegisterDto
    {
        [StringLength(maximumLength: 32, ErrorMessage = "Must be at least 4 to 32 characters.", MinimumLength = 4)]
        public required string Username { get; set; }

        [StringLength(maximumLength: 32, ErrorMessage = "Must be at least 8 to 32 characters.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Must be at least 2 to 64 characters.", MinimumLength = 2)]
        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
    }
}
