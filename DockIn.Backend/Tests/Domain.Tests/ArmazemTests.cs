using DockIn.Domain;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

public class ArmazemTests
{
    public static readonly Random random = new Random();

    [Fact]
    public void CriacaoArmazem_Ok()
    {
        Armazem armazem = new Armazem(Localizacao.Porto);

        Assert.True(armazem != null);
    }

    [Fact]
    public void CriacaoStock_Artigo_Nao_Existe()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo("Sumol", 5, 5, ArtigoClasses.B);

        armazem.AdicionarStock(artigo, 5, 5);
        int StockArmazem = armazem.Stock.Count;

        Assert.Equal(1, StockArmazem);
    }

    [Fact]
    public void AdicionarQuantidade_StockSob_MesmoArtigo()
    {
        Armazem armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo("Sumol", 5, 5, ArtigoClasses.B);

        armazem.AdicionarStock(artigo, 5, 5);
        armazem.AdicionarStock(artigo, 3, 5);

        Stock stock = armazem.Stock.First();

        Assert.Equal(8, stock.Quantidade);
        Assert.Single(armazem.Stock);
    }

    [Fact]
    public void RemoverStock_MinimoViavel_ManterSoftDelete()
    {
        Armazem armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo("Sumol", 5, 5, ArtigoClasses.B);

        armazem.AdicionarStock(artigo, 5, 5);
        armazem.RemoverStock(artigo, 5);

        Stock stock = armazem.Stock.First();

        Assert.Equal(0, stock.Quantidade);
        Assert.Single(armazem.Stock);
    }

    [Fact]
    public void LugaresDisponiveis_CalculoCoerente()
    {
        var armazem = new Armazem(Localizacao.Porto);

        var artigoA = new Artigo(0, "Coca-Colca", 5, 5, ArtigoClasses.A);
        var artigoB = new Artigo(1, "Coca-Colca", 5, 5, ArtigoClasses.A);

        armazem.AdicionarStock(artigoA, 5, 5);
        armazem.AdicionarStock(artigoB, 5, 5);

        int stock = armazem.Stock.Sum(s => s.Quantidade);

        Assert.Equal(stock, armazem.OcupacaoArmazem());
    }

    [Fact]
    public void Limite_CapacidadeExcedido_PorUm()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Armazem(Localizacao.Porto, 1000));
    }

    [Fact]
    public void Limite_Capacidade_Invalido_Negativo()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Armazem(Localizacao.Porto, -1));
    }

    [Fact]
    public void IncrementarStock_Quantidade_InvalidaSuperior()
    {
        var armazem = new Armazem(Localizacao.Porto, 999);
        var artigoA = new Artigo(0, "Coca-Colca", 5, 5, ArtigoClasses.A);

        artigoA.AtualizarCapacidadeMax_Artigo(10000);

        armazem.AdicionarStock(artigoA, 998, 5);
        Stock stock = armazem.Stock.First(s => s.Quantidade > 0);

        Assert.Equal(998, stock.Quantidade);

        Assert.Throws<DomainException>(() => armazem.AdicionarStock(artigoA, 2, 5));
    }

}
