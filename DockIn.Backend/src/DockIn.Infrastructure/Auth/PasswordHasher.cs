using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace DockIn.Infrastructure.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string plainText)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText);
        }

        public bool verifyPassword(string plainText, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, passwordHash);
        }
    }
}
