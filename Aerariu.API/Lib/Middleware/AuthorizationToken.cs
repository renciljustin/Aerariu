namespace Aerariu.API.Lib.Middleware
{
    public class AuthorizationToken
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
