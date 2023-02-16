using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Utils.Constants
{
    public class ErrorMessage
    {
        public const string User_DuplicateUsername = "Username is already used.";
        public const string User_InvalidCredentials = "Invalid username or password.";
        public const string User_NotFound = "Cannot find the user.";

        public const string RefreshToken_Expired = "The refresh token is expired.";
        public const string RefreshToken_Invalid = "The refresh token is invalid.";
    }
}
