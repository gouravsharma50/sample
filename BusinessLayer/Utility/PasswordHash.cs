using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Utility
{
    public class PasswordHash
    {
        // The following constants may be changed without breaking existing hashes.
        /// <summary>
        ///     The salt byte size
        /// </summary>
        public const int SaltByteSize = 24;

        /// <summary>
        ///     The hash byte size
        /// </summary>
        public const int HashByteSize = 24;

        /// <summary>
        ///     The PBKDF2 iterations
        /// </summary>
        public const int Pbkdf2Iterations = 1000;

        /// <summary>
        ///     The iteration index
        /// </summary>
        public const int IterationIndex = 0;

        /// <summary>
        ///     The salt index
        /// </summary>
        public const int SaltIndex = 1;

        /// <summary>
        ///     The PBKDF2 index
        /// </summary>
        public const int Pbkdf2Index = 2;

        /// <summary>
        ///     Creates a salted PBKDF2 hash of the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash of the password.</returns>
        public static string CreateHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(password))
            {
                return password;
            }
            else
            {
                // Generate a random salt
                var csprng = new RNGCryptoServiceProvider();
                var salt = new byte[SaltByteSize];
                csprng.GetBytes(salt);

                // Hash the password and encode the parameters
                var hash = Pbkdf2(password, salt, Pbkdf2Iterations, HashByteSize);
                return $"{Pbkdf2Iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
            }
        }

        /// <summary>
        ///     Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="correctHash">A hash of the correct password.</param>
        /// <returns>True if the password is correct. False otherwise.</returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            try
            {
                char[] delimiter = { ':' };
                var split = correctHash.Split(delimiter);
                var iterations = int.Parse(split[IterationIndex]);
                var salt = Convert.FromBase64String(split[SaltIndex]);
                var hash = Convert.FromBase64String(split[Pbkdf2Index]);

                if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(password))
                {
                    return false;
                }
                else
                {
                    var testHash = Pbkdf2(password, salt, iterations, hash.Length);
                    return SlowEquals(hash, testHash);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Compares two byte arrays in length-constant time. This comparison
        ///     method is used so that password hashes cannot be extracted from
        ///     on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        /// <summary>
        ///     Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}