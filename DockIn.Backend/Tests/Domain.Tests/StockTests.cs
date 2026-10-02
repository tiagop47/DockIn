using DockIn.Domain;
using DockIn.Domain.Entities;

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
        var artigo = new Artigo(10m, 5m, UnidadeMedida.Unidade);
        Assert.Throws<ArgumentNullException>(() => new Stock(null!, artigo, 5, 5));
    }

    [Fact]
    public void IncrementarQuantidade_ComValorValido_DeveAumentarQuantidadeEVersao()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo(10m, 5m, UnidadeMedida.Unidade);
        var stock = new Stock(armazem, artigo, preco: 5, quantidade: 10);

        stock.IncrementarQuantidade(5);

        Assert.Equal(15, stock.Quantidade);
        Assert.Equal(2, stock.Versao);
    }

    [Fact]
    public void DecrementarQuantidade_ComValorValido_DeveReduzirQuantidadeEVersao()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo(10m, 5m, UnidadeMedida.Unidade);
        var stock = new Stock(armazem, artigo, preco: 5, quantidade: 10);

        stock.DecrementarQuantidade(4);

        Assert.Equal(6, stock.Quantidade);
        Assert.Equal(2, stock.Versao);
    }

    [Fact]
    public void DecrementarQuantidade_MaisDoQueExiste_DeveLancarExcecao()
    {
        var armazem = new Armazem(Localizacao.Porto);
        var artigo = new Artigo(10m, 5m, UnidadeMedida.Unidade);
        var stock = new Stock(armazem, artigo, preco: 5, quantidade: 5);

        // Tentar retirar 10 quando só há 5 não pode permitir saldo negativo!
        Assert.Throws<ArgumentOutOfRangeException>(() => stock.DecrementarQuantidade(10));
    }
}
