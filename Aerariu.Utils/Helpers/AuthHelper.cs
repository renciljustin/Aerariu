using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Utils.Helpers
{
    public static class AuthHelper
    {
        public static class Policy
        {
            public const string RequiresAdmin = "RequiresAdmin";
            public const string RequiresUser = "RequiresUser";
        }

        public static class Role
        {
            public const string Administrator = "Administrator";
            public const string User = "User";
        }
    }
}
