using System.ComponentModel.DataAnnotations;

namespace Aerariu.API.Dtos
{
    public class UserLoginDto
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
