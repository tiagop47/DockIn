namespace Domain.Tests;

using DockIn.Domain;
using Xunit;

public class ArtigoTests
{
    [Fact]
    public void Criar_Artigo_ComValoresValidos_DeveInicializarPropriedadesCorretamente()
    {
        // Arrange
        var peso = 10.5m;
        var dimensoes = 20.0m;
        var unidade = UnidadeMedida.Quilograma;
        var antes = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var artigo = new Artigo(peso, dimensoes, unidade);

        // Assert
        Assert.Equal(0, artigo.ArtigoId);
        Assert.Equal(peso, artigo.Peso);
        Assert.Equal(dimensoes, artigo.Dimensoes);
        Assert.Equal(unidade, artigo.UnidadeMedida);
        Assert.False(artigo.AtivoBool);
        Assert.True(artigo.CreatedAt >= antes && artigo.CreatedAt <= DateTime.UtcNow.AddSeconds(1));
    }

    [Fact]
    public void Criar_Artigo_ComConstrutorInterno_DeveAtribuirIdEPropriedades()
    {
        // Arrange & Act
        var artigo = new Artigo(42, 5.0m, 15.0m, UnidadeMedida.Caixa);

        // Assert
        Assert.Equal(42, artigo.ArtigoId);
        Assert.Equal(5.0m, artigo.Peso);
        Assert.Equal(15.0m, artigo.Dimensoes);
        Assert.Equal(UnidadeMedida.Caixa, artigo.UnidadeMedida);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-99.9)]
    public void Criar_Artigo_PesoInvalido_DeveLancarArgumentOutOfRangeException(decimal pesoInvalido)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Artigo(pesoInvalido, 10m, UnidadeMedida.Unidade));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-50.5)]
    public void Criar_Artigo_DimensoesInvalidas_DeveLancarArgumentOutOfRangeException(decimal dimensoesInvalidas)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Artigo(5m, dimensoesInvalidas, UnidadeMedida.Unidade));
    }

    [Fact]
    public void Criar_Artigo_UnidadeMedidaInvalida_DeveLancarArgumentOutOfRangeException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Artigo(5m, 10m, (UnidadeMedida)999));
    }

    [Fact]
    public void Ativar_DeveAlterarAtivoBoolParaTrue()
    {
        // Arrange
        var artigo = new Artigo(5m, 10m, UnidadeMedida.Unidade);
        Assert.False(artigo.AtivoBool);

        // Act
        artigo.Ativar();

        // Assert
        Assert.True(artigo.AtivoBool);
    }

    [Fact]
    public void Desativar_DeveAlterarAtivoBoolParaFalse()
    {
        // Arrange
        var artigo = new Artigo(5m, 10m, UnidadeMedida.Unidade);
        artigo.Ativar();
        Assert.True(artigo.AtivoBool);

        // Act
        artigo.Desativar();

        // Assert
        Assert.False(artigo.AtivoBool);
    }

    [Fact]
    public void AtualizarDimensoes_ComValorValido_DeveAtualizarDimensoes()
    {
        // Arrange
        var artigo = new Artigo(5m, 10m, UnidadeMedida.Litro);

        // Act
        artigo.AtualizarDimensoes(25.5m);

        // Assert
        Assert.Equal(25.5m, artigo.Dimensoes);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void AtualizarDimensoes_ComValorInvalido_DeveLancarExcecaoENaoAlterar(decimal valorInvalido)
    {
        // Arrange
        var artigo = new Artigo(5m, 10m, UnidadeMedida.Litro);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => artigo.AtualizarDimensoes(valorInvalido));
        Assert.Equal(10m, artigo.Dimensoes);
    }

    [Fact]
    public void AtualizarPeso_ComValorValido_DeveAtualizarPeso()
    {
        // Arrange
        var artigo = new Artigo(5m, 10m, UnidadeMedida.Quilograma);

        // Act
        artigo.AtualizarPeso(12.7m);

        // Assert
        Assert.Equal(12.7m, artigo.Peso);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void AtualizarPeso_ComValorInvalido_DeveLancarExcecaoENaoAlterar(decimal valorInvalido)
    {
        // Arrange
        var artigo = new Artigo(5m, 10m, UnidadeMedida.Quilograma);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => artigo.AtualizarPeso(valorInvalido));
        Assert.Equal(5m, artigo.Peso);
    }

    [Fact]
    public void Equals_ArtigosComMesmoIdPositivo_DevemSerIguais()
    {
        // Arrange
        var artigo1 = new Artigo(1, 5m, 10m, UnidadeMedida.Unidade);
        var artigo2 = new Artigo(1, 20m, 30m, UnidadeMedida.Caixa);

        // Act & Assert
        Assert.True(artigo1.Equals(artigo2));
        Assert.Equal(artigo1.GetHashCode(), artigo2.GetHashCode());
    }

    [Fact]
    public void Equals_ArtigosComIdsDiferentes_NaoDevemSerIguais()
    {
        // Arrange
        var artigo1 = new Artigo(1, 5m, 10m, UnidadeMedida.Unidade);
        var artigo2 = new Artigo(2, 5m, 10m, UnidadeMedida.Unidade);

        // Act & Assert
        Assert.False(artigo1.Equals(artigo2));
    }

    [Fact]
    public void Equals_ArtigosSemId_InstanciasDiferentes_NaoDevemSerIguais()
    {
        // Arrange
        var artigo1 = new Artigo(5m, 10m, UnidadeMedida.Unidade);
        var artigo2 = new Artigo(5m, 10m, UnidadeMedida.Unidade);

        // Act & Assert
        Assert.False(artigo1.Equals(artigo2));
    }

    [Fact]
    public void Equals_MesmaInstancia_DeveSerIgual()
    {
        // Arrange
        var artigo = new Artigo(5m, 10m, UnidadeMedida.Unidade);

        // Act & Assert
        Assert.True(artigo.Equals(artigo));
    }

    [Fact]
    public void Equals_ComparadoComNullOuOutroTipo_DeveRetornarFalse()
    {
        // Arrange
        var artigo = new Artigo(5m, 10m, UnidadeMedida.Unidade);

        // Act & Assert
        Assert.False(artigo.Equals(null));
        Assert.False(artigo.Equals("outra_coisa"));
    }
}
