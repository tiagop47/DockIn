namespace DockIn.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public Role Role { get; private set; }
        private bool ativoFlag;
        public bool AtivoFlag
        {
            get => ativoFlag;
            private set
            {
                if(ativoFlag == value)
                {
                    return;
                }

                ativoFlag = value;
            }
        }
        public string Email { get; private set; }
        public string PasswordHash { get; }
        public DateTime CreatedAt { get; }

        public User(string email, Role role, string passwordHash)
        {
            TrocarEmail(email);
            Role = role;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }

        internal User(int id, string email, Role role, string passwordHash)
        {
            Email = null!;
            UserId = id;
            TrocarEmail(email);
            Role = role;
            PasswordHash = passwordHash;
        }

        private User()
        {
            PasswordHash = null!;
            Email = null!;
        }

        public void TrocarEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new DomainException(nameof(email));
            }

            if(!email.ContainsAny<char>('@', '.'))
            {
                throw new DomainException(nameof(email));
            }

            Email = email;
        }
    }
}
