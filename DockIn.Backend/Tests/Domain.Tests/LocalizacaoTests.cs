namespace Domain.Tests;

using DockIn.Domain.Entities;
using Xunit;

public class LocalizacaoTests
{
    [Fact]
    public void CriarLocalizacao_ComDadosValidos_DeveInicializarComSucesso()
    {
        var localizacao = new Localizacao(1, "A-01", "Rack");

        Assert.Equal(1, localizacao.ArmazemId);
        Assert.Equal("A-01", localizacao.Codigo);
        Assert.Equal("Rack", localizacao.Tipo);
        Assert.True(localizacao.Ativo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CriarLocalizacao_ComArmazemInvalido_DeveLancarDomainException(int armazemIdInvalido)
    {
        Assert.Throws<DomainException>(() => new Localizacao(armazemIdInvalido, "A-01"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CriarLocalizacao_SemCodigo_DeveLancarDomainException(string? codigoInvalido)
    {
        Assert.Throws<DomainException>(() => new Localizacao(1, codigoInvalido!));
    }

    [Fact]
    public void AlterarDados_Valido_DeveAtualizarPropriedades()
    {
        var localizacao = new Localizacao(1, "A-01", "Rack");
        localizacao.AlterarDados("B-02", "Piso");

        Assert.Equal("B-02", localizacao.Codigo);
        Assert.Equal("Piso", localizacao.Tipo);
    }

    [Fact]
    public void InativarEAtivar_DeveAlterarEstado()
    {
        var localizacao = new Localizacao(1, "A-01");
        Assert.True(localizacao.Ativo);

        localizacao.Desativar();
        Assert.False(localizacao.Ativo);

        localizacao.Ativar();
        Assert.True(localizacao.Ativo);
    }
    
}