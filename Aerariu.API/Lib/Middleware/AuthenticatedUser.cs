namespace Aerariu.API.Lib.Middleware
{
    public class AuthenticatedUser
    {
        public required UserInfo User { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }

    public class UserInfo
    {
        public Guid Id { get; set; }
        public required string Username { get; set;}
        public required string Email { get; set;}
    }
}
