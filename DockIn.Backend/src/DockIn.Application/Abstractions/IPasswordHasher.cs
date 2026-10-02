public interface IPasswordHasher
{
    string HashPassword(string plainText);
    bool verifyPassword(string plainText, string passwordHash);
}
