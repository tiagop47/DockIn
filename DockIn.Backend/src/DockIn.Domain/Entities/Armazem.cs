using DockIn.Domain;

public class Armazem
{
    public int ArmazemId { get; }

    public Localizacao? Localizacao { get; private set; }

    public IReadOnlyCollection<Stock> Stock => _stocks.AsReadOnly();
    private List<Stock> _stocks = new();

    private int _capacidadeMax;
    public int CapacidadeMaxima
    {
        get => _capacidadeMax;

        private set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Valor de itens");
            }

            if (value > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Impossivel armazenar tantos itens");
            }

            _capacidadeMax = value;
        }
    }

    protected Armazem() { }

    public Armazem(Localizacao? localizacao, int capacidade = 10)
    {
        if (localizacao == null)
        {
            throw new ArgumentNullException(nameof(localizacao), "Localização Obrigatória");
        }
        else
        {
            Localizacao = localizacao;
        }

        CapacidadeMaxima = capacidade;
    }

    public int OcupacaoArmazem()
    {
        int capacidadeTotalItems = _stocks.Sum(s => s.CapacidadeMaxima);
        if (capacidadeTotalItems <= 0)
        {
            return 0;
        }

        int totalItems = _stocks.Sum(s => s.Quantidade);

        return totalItems;
    }

    public int LugaresDisponiveis()
    {
        return CapacidadeMaxima - _stocks.Count(s => s.Quantidade > 0);
    }

    public void AdicionarStock(Artigo artigo, int quantidade, double preco)
    {
        if (artigo == null)
        {
            throw new ArgumentNullException("Não podes passar um artigo null");
        }

        if (_stocks.Sum(s => s.Quantidade) + quantidade > CapacidadeMaxima)
        {
            throw new DomainException("Capacidade do Armazém foi excedida");
        }

        var stock = _stocks.FirstOrDefault(s => s.ArtigoId == artigo.ArtigoId);

        if (stock != null)
        {
            stock.IncrementarQuantidade(quantidade);
        }
        else
        {
            Stock novoStock = new Stock(this, artigo, preco, quantidade);
            _stocks.Add(novoStock);
        }
    }

    public void RemoverStock(Artigo artigo, int quantidade)
    {
        if (artigo == null)
        {
            throw new ArgumentNullException("Não podes passar um artigo null");
        }

        var stock = _stocks.FirstOrDefault(s => s.ArtigoId == artigo.ArtigoId);

        if (stock == null)
        {
            throw new ArtigoNaoEncontradoException(artigo.ArtigoId);
        }

        stock.DecrementarQuantidade(quantidade);
    }
}
