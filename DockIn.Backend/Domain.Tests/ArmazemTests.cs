using DockIn.Domain;

public class ArmazemTests
{
    public static readonly Random random = new Random();
    private static IReadOnlyCollection<Artigo> Criar_5_Artigos()
    {
        List<Artigo> artigos = new();
        int n = 5;

        for (int i = 0; i < n; i++)
        {
            artigos.Add(new Artigo(i + 1, $"Artigo {i}", 12, 12, ArtigoClasses.A));

            if (i == 3)
                artigos[i].Description = "Coca Cola";
        }

        return artigos;
    }

    private static Armazem CriarArmazem()
    {
        return new Armazem(Localizacao.Porto);
    }

    private static IReadOnlyCollection<Stock> CriarStocks(Armazem armazem, IEnumerable<Artigo> artigos)
    {

        List<Stock> stocks = new();

        foreach (var item in artigos)
        {
            stocks.Add(new Stock(armazem, item, random.NextInt64(), 5));
        }

        return stocks;
    }

    [Fact]
    public void CriacaoArmazem_Ok_1()
    {
        Armazem armazem = new Armazem(Localizacao.Porto);

        Assert.True(armazem != null);
    }

    [Fact]
    public void CriacaoStock()
    {
        Armazem armazem = new Armazem(Localizacao.Porto);

        IReadOnlyCollection<Artigo> teste = Criar_5_Artigos();

        foreach (var item in teste)
        {
            armazem.AdicionarStock(item, 5, 10);
        }

        Assert.Equal(5, armazem.OcupacaoArmazem());
    }

    [Fact]
    public void RemoverStock()
    {
        Armazem armazem = new Armazem(Localizacao.Porto);

        IReadOnlyCollection<Artigo> teste = Criar_5_Artigos();

        foreach (var item in teste)
        {
            armazem.AdicionarStock(item, 5, 10);
        }

        Artigo temporaria = teste.First(s => s.Description.Equals("Coca Cola"));

        armazem.RemoverStock(temporaria, 1);
        Assert.Equal(5, armazem.LugaresDisponiveis());
    }

    [Fact]
    public void QuantidadeStocks()
    {
        Armazem armazem = CriarArmazem();
        IReadOnlyCollection<Artigo> artigos = Criar_5_Artigos();

        IReadOnlyCollection<Stock> stocks = CriarStocks(armazem, artigos);
        Assert.Equal(5, stocks.Count);
    }


    [Fact]
    public void QuantidadeStock_Deve_Aumentar_6()
    {
        Armazem armazem = CriarArmazem();

        IReadOnlyCollection<Artigo> artigos = Criar_5_Artigos();
        foreach (var item in artigos)
        {
            Console.WriteLine(item.ArtigoId);
        }
        IReadOnlyCollection<Stock> stocks = CriarStocks(armazem, artigos);

        Assert.Equal(5, stocks.Count);

        Artigo novo6 = new Artigo("teste", 5, 5, ArtigoClasses.A);

        foreach (var item in artigos)
        {
            armazem.AdicionarStock(item, 5, 5);
        }

        Assert.Equal(5, armazem.Stock.Count);

        armazem.AdicionarStock(novo6, 5, 5);

        Assert.Equal(6, armazem.Stock.Count);
    }
}
