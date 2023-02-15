using System.Security.Cryptography;

namespace Aerariu.Utils.Tools
{
    public class PasswordHasher
    {
        // The number of iterations used to derive the key
        private const int Iterations = 10000;

        // The size of the salt in bytes
        private const int SaltSize = 16;

        // The size of the derived key in bytes
        private const int KeySize = 32;

        // Hashes the password using a secure algorithm
        public static async Task<(string passwordHash, string salt)> HashPasswordAsync(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] key = await Task.Run(() => deriveBytes.GetBytes(KeySize));
                string passwordHash = Convert.ToBase64String(key);
                string saltString = Convert.ToBase64String(salt);
                return (passwordHash, saltString);
            }
        }

        // Verify that a password matches the hashed password
        public static async Task<bool> VerifyPasswordAsync(string password, string passwordHash, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
            {
                byte[] key = await Task.Run(() => deriveBytes.GetBytes(KeySize));
                string passwordHashToCheck = Convert.ToBase64String(key);
                return passwordHash == passwordHashToCheck;
            }
        }
    }
}
