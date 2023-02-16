using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Utils.Helpers
{
    public static class StringHelper
    {
        public static string GenerateBase64String()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static async Task<string> GenerateBase64StringAsync()
            => await Task.Run(() => GenerateBase64String());
        
    }
}
