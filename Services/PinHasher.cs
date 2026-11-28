using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Ubuntu_Intelli_Compass.Services.Interfaces;

namespace Ubuntu_Intelli_Compass.Services
{
    public class PinHasher : IPinHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        private readonly HashAlgorithmName algorithmName = HashAlgorithmName.SHA512;

        public string Hash(string pin)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(pin, salt, Iterations, algorithmName, HashSize);
             
            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string pin, string pinHash)
        {
            string[] parts = pinHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(pin, salt, Iterations, algorithmName, HashSize);

            //return hash.SequenceEqual(inputHash);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
