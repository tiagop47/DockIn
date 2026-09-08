using DockIn.Domain;

public class StockTests
{
    [Fact]
    public void Criar_Stock_Com_Nulls()
    {
        Assert.Throws<ArgumentNullException>(() => new Stock(null!, null!, 5, 5));
    }

    [Fact]
    public void CriarStock_Armazem_Null()
    {
        var artigo = new Artigo("Coca Cola", 10, 10, ArtigoClasses.A);
        Assert.Throws<ArgumentNullException>(() => new Stock(null!, artigo, 5, 5));
    }

    [Fact]
    public void CriarStock_QuantidadeInicial_MaiorQueCapacidadeMaxima_DeveLancarExcecao()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo("Coca Cola", 10, 10, ArtigoClasses.A); // QUANTIDADE_MAX = 100

        // Tentar criar com 150 unidades quando a capacidade máxima do artigo é 100
        Assert.Throws<ArgumentOutOfRangeException>(() => new Stock(armazem, artigo, preco: 5.0, quantidade: 150));
    }

    [Fact]
    public void IncrementarQuantidade_ComValorValido_DeveAumentarQuantidadeEVersao()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo("Coca Cola", 10, 10, ArtigoClasses.A);
        var stock = new Stock(armazem, artigo, preco: 5.0, quantidade: 10);

        stock.IncrementarQuantidade(5);

        Assert.Equal(15, stock.Quantidade);
        Assert.Equal(2, stock.Versao);
    }

    [Fact]
    public void IncrementarQuantidade_AcimaDaCapacidadeMaxima_DeveLancarExcecao()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo("Coca Cola", 10, 10, ArtigoClasses.A); // QUANTIDADE_MAX = 100
        var stock = new Stock(armazem, artigo, preco: 5.0, quantidade: 90);

        // 90 + 20 = 110 > 100
        Assert.Throws<ArgumentOutOfRangeException>(() => stock.IncrementarQuantidade(20));
    }

    [Fact]
    public void DecrementarQuantidade_ComValorValido_DeveReduzirQuantidadeEVersao()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo("Coca Cola", 10, 10, ArtigoClasses.A);
        var stock = new Stock(armazem, artigo, preco: 5.0, quantidade: 10);

        stock.DecrementarQuantidade(4);

        Assert.Equal(6, stock.Quantidade);
        Assert.Equal(2, stock.Versao);
    }

    [Fact]
    public void DecrementarQuantidade_MaisDoQueExiste_DeveLancarExcecao()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo("Coca Cola", 10, 10, ArtigoClasses.A);
        var stock = new Stock(armazem, artigo, preco: 5.0, quantidade: 5);

        // Tentar retirar 10 quando só há 5 não pode permitir saldo negativo!
        Assert.Throws<ArgumentOutOfRangeException>(() => stock.DecrementarQuantidade(10));
    }
}
