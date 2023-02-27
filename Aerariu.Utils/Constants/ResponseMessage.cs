namespace Aerariu.Utils.Constants
{
    public class ResponseMessage
    {
        //Auth
        public const string GeneratedNewToken = "Generated a new token.";
        public const string RegistrationSuccess = "Registered successfully.";
        public static string LoginSuccess(string user) => $"Logged in with user {user}.";
    }
}
