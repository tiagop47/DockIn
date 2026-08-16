using DockIn.Domain;

public class StockTests
{
    List<Artigo> _artigos = new List<Artigo>();

    private static Artigo CriarArtigo()
    {
        return new Artigo("Garrafa de Água",
            199,
            999,
            ArtigoClasses.A);
    }

    private static Artigo CriarArtigo1()
    {
        return new Artigo("Coca-Cola",
            199,
            199,
            ArtigoClasses.B);
    }



    private static List<Artigo> ListaDeStock()
    {
        List<Artigo> lista = new List<Artigo>();

        Artigo artigo1 = CriarArtigo();
        Artigo artigo2 = CriarArtigo1();

        lista.Add(artigo1);
        lista.Add(artigo2);

        return lista;
    }

    [Fact]
    public void Colocar_Artigo_Em_Stock()
    {
        Stock tmp = new Stock(CriarArtigo(), 12, 15);
        double preco = 12;
        int quantidade = 15;

        Assert.NotNull(tmp);

        Assert.Equal(preco, tmp.Preco);
        Assert.Equal(quantidade, tmp.Quantidade);
    }

    [Fact]
    public void Stocks_Sao_Iguais()
    {
        var artigo = CriarArtigo();

        Stock tmp = new Stock(artigo, 12, 15);
        Stock tmp1 = new Stock(artigo, 12, 15);

        Assert.Equal(tmp, tmp1);
    }
}
