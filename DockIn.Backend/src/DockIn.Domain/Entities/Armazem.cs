using System.Diagnostics;
using DockIn.Domain;

public class Armazem
{
    public int ArmazemId { get; }

    public string Localizacao { get; private set; } = string.Empty;

    public IReadOnlyCollection<Stock> Stock => _stocks.AsReadOnly();
    private List<Stock> _stocks = new();

    public int CapacidadeMaxima { get; private set; }

    public Armazem() { }

    public Armazem(string localizacao)
    {
        Localizacao = localizacao;
        CapacidadeMaxima = 5;
    }

    public int OcupacaoArmazem()
    {
        return _stocks.Count;
    }

    public int PosicoesDisponiveis()
    {
        return CapacidadeMaxima - _stocks.Count;
    }

    public void AdicionarStock(Artigo artigo, int quantidade, double preco)
    {
        var stock = _stocks.FirstOrDefault(s => s.ArtigoId == artigo.ArtigoId);

        if (stock != null)
        {
            stock.IncrementarQuantidade(quantidade);
        }
        else
        {
            if (_stocks.Count >= CapacidadeMaxima)
            {
                throw new DomainException("Capacidade do Armazém foi excedida");
            }

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
