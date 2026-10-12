namespace DockIn.Domain.Entities
{
    public class User
    {
        public int UserId { get; private set; }
        public Perfil PerfilUser { get; private set; }
        public bool Ativo { get; private set; }
        private string _email = null!;

        public string Email
        {
            get => _email;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new DomainException("Email obrigatório.");
                }

                value = value.Trim();

                if (value.Length > 254)
                {
                    throw new DomainException("Email inválido.");
                }

                if (!value.Contains('@') || !value.Contains('.'))
                {
                    throw new DomainException(nameof(value));
                }

                _email = value.ToLowerInvariant();
            }
        }
        public string PasswordHash { get; private set; } = null!;
        public DateTime CreatedAt { get; }

        public User(string email, Perfil role, string passwordHash)
        {
            Email = email;
            PerfilUser = role;
            PasswordHash = passwordHash;
            Ativo = false;
            CreatedAt = DateTime.UtcNow;
        }

        internal User(int id, string email, Perfil role, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new DomainException(nameof(passwordHash));
            }
            UserId = id;
            Email = email;
            PerfilUser = role;
            PasswordHash = passwordHash;
        }

        private User()
        {
            _email = null!;
            PasswordHash = null!;
        }

        public void TrocarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainException(email);
            }

            Email = email;
        }

        public void AtualizarPasswordHash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new DomainException(nameof(password));
            }

            PasswordHash = password;
        }

        public void AtribuirPerfil(Perfil perfil)
        {
            if (!Enum.IsDefined(perfil))
            {
                throw new DomainException(nameof(perfil));
            }

            if (PerfilUser == perfil)
            {
                return;
            }

            PerfilUser = perfil;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }
    }
}
