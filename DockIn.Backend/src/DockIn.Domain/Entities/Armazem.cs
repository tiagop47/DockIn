namespace DockIn.Domain.Entities
{
    public class Armazem
    {
        public int ArmazemId { get; }

        public Localizacao Localizacao { get; private set; }

        public IReadOnlyCollection<Stock> Stock => _stocks.AsReadOnly();
        private readonly List<Stock> _stocks = new();

        private int _capacidadeMax;
        public int CapacidadeMaxima
        {
            get => _capacidadeMax;

            private set
            {
                if (value < 0 && value > 999)
                {
                    throw new ArgumentOutOfRangeException(paramName: nameof(value), "Valor de itens");
                }

                _capacidadeMax = value;
            }
        }

        protected Armazem() { }

        internal Armazem(int id, Localizacao localizacao, int capacidade = 10)
        {
            ArmazemId = id;
            Localizacao = localizacao;
            CapacidadeMaxima = capacidade;
        }

        public Armazem(Localizacao localizacao, int capacidade = 10)
        {
            Localizacao = localizacao;
            CapacidadeMaxima = capacidade;
        }

        public int LugaresDisponiveis()
        {
            return CapacidadeMaxima - _stocks.Count(s => s.Quantidade > 0);
        }

        public Stock AdicionarStock(Artigo artigo, int quantidade, decimal preco)
        {
            ArgumentNullException.ThrowIfNull(artigo);

            if (_stocks.Sum(s => s.Quantidade) + quantidade > CapacidadeMaxima)
            {
                throw new DomainException("Capacidade do Armazém foi excedida");
            }

            foreach (var item in _stocks)
            {
                item.IncrementarQuantidade(quantidade);
            }

            Stock novoStock = new Stock(this, artigo, preco, quantidade);
            return novoStock;
        }

        public Stock RemoverStock(Artigo artigo, int quantidade)
        {
            if (artigo == null)
            {
                throw new ArgumentNullException("Não podes passar um artigo null");
            }

            if (quantidade <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidade));
            }

            var stock = _stocks
                .Where(s => s.ArtigoId == artigo.ArtigoId && s.Quantidade > 0)
                .OrderBy(s => s.CreatedAt)
                .FirstOrDefault();

            if (stock is null)
            {
                throw new ArtigoNaoEncontradoException(artigo.ArtigoId);
            }

            var restante = quantidade;
            Stock primeiroStock = stock;

            foreach (var lote in _stocks
                .Where(s => s.ArtigoId == artigo.ArtigoId && s.Quantidade > 0)
                .OrderBy(s => s.CreatedAt))
            {
                var remover = Math.Min(lote.Quantidade, restante);
                lote.DecrementarQuantidade(remover);
                restante -= remover;

                if (restante == 0)
                    break;
            }

            if (restante > 0)
                throw new DomainException("Quantidade de stock insuficiente.");

            return primeiroStock;
        }
    }
}
