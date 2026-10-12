namespace DockIn.Domain.Entities
{
    public class Localizacao
    {
        public int LocalizacaoId { get; private set; }
        public int ArmazemId { get; private set; }
        public string Codigo { get; private set; }
        public string Tipo { get; private set; }
        public bool Ativo { get; private set; }

        protected Localizacao()
        {
            Codigo = null;
            Tipo = null!;
        }

        public Localizacao(int armazemId, string codigo, string tipo = "Standard")
        {
            if (armazemId <= 0)
            {
                throw new DomainException("O armazém associado é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new DomainException("O código da localização é obrigatório.");
            }

            ArmazemId = armazemId;
            Codigo = codigo.Trim().ToUpperInvariant();
            Tipo = string.IsNullOrWhiteSpace(tipo) ? "Standard" : tipo.Trim();
            Ativo = true;
        }

        internal Localizacao(int localizacaoId, int armazemId, string codigo, string tipo, bool ativo)
            : this(armazemId, codigo, tipo)
        {
            LocalizacaoId = localizacaoId;
            Ativo = ativo;
        }

        public void AlterarDados(string codigo, string tipo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new DomainException("O código da localização é obrigatório.");
            }

            Codigo = codigo.Trim().ToUpperInvariant();
            Tipo = string.IsNullOrWhiteSpace(tipo) ? "Standard" : tipo.Trim();
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
