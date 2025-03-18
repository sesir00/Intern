using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Demofirst.Authentication
{
	public static class Auth
	{
        public static string HashPassword(string password)
        {
            const int SaltSize = 16;   // Salt size in bytes (128 bits)
            const int HashSize = 32;   // Hash size in bytes (256 bits)
            const int Iterations = 10000;

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] fixedSalt = Encoding.UTF8.GetBytes("MyFixedSalt123456"); // 16-byte fixed salt
                //rng.GetBytes(fixedSalt); // Generate random salt

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, fixedSalt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize); // Generate the hash

                    // Combine salt and hash for storage
                    return Convert.ToBase64String(Combine(fixedSalt, hash));
                }
            }
        }

        private static byte[] Combine(byte[] salt, byte[] hash)
        {
            byte[] combined = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, combined, 0, salt.Length);
            Array.Copy(hash, 0, combined, salt.Length, hash.Length);
            return combined;
        }
    }
}