namespace DockIn.Domain.Entities
{
    public class Armazem
    {
        public int ArmazemId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public bool Ativo { get; private set; }

        protected Armazem()
        {
            Nome = null!;
        }

        public Armazem(string nome, string? descricao = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("O nome do armazém é obrigatório.");
            }

            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            Ativo = true;
        }

        internal Armazem(int armazemId, string nome, string? descricao = null, bool ativo = true)
            : this(nome, descricao)
        {
            ArmazemId = armazemId;
            Ativo = ativo;
        }


        public void AlterarDados(string nome, string? descricao = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("O nome do armazém é obrigatório.");
            }

            Nome = nome.Trim();
            Descricao = descricao?.Trim();
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
