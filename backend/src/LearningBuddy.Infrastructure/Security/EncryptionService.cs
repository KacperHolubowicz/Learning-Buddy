using LearningBuddy.Application.Common.Interfaces.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace LearningBuddy.Infrastructure.Security
{
    public class EncryptionService : IEncryptionService
    {
        private readonly int iterationPbkdf2;
        private readonly int keyBytes;

        public EncryptionService(IConfiguration configuration)
        {
            iterationPbkdf2 = int.Parse(configuration["Security:Encryption:Iterations"]);
            keyBytes = int.Parse(configuration["Security:Encryption:KeyBytes"]);
        }

        public Tuple<byte[], byte[]> HashPasswordWithNewSalt(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: iterationPbkdf2,
                numBytesRequested: keyBytes
            );
            return new Tuple<byte[], byte[]>(hashed, salt);
        }

        public bool CheckPassword(string plainPassword, byte[] salt, byte[] hashedPassword)
        {
            byte[] hashedPlain = KeyDerivation.Pbkdf2(
                password: plainPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: iterationPbkdf2,
                numBytesRequested: keyBytes
            );

            return hashedPlain.SequenceEqual(hashedPassword);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[keyBytes];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(salt);
            return salt;
        }
    }
}
