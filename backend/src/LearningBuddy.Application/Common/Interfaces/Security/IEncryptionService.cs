namespace LearningBuddy.Application.Common.Interfaces.Security
{
    public interface IEncryptionService
    {
        Tuple<byte[], byte[]> HashPasswordWithNewSalt(string password);
        bool CheckPassword(string plainPassword, byte[] salt, byte[] hashedPassword);
    }
}
