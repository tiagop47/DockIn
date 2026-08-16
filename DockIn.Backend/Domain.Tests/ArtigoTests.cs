namespace Domain.Tests;

using DockIn.Domain;
using Xunit.Sdk;

public class ArtigoTests
{

    [Fact]
    public void Criar_Artigo_Valores_ECP_1()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Artigo(
        "Garrafa de Água",
         199999,
         999,
         ArtigoClasses.A));
    }

    [Fact]
    public void Criar_Artigo_Valores_ECP_2()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Artigo(
        "Garrafa de Água",
         999,
         12213,
         ArtigoClasses.A));
    }

    [Fact]
    public void ExigeValide_ClasseECP_3()
    {
        var artigo = new Artigo(
        "Garrafa de Água",
         999,
         122,
         ArtigoClasses.A);

        Assert.False(artigo.ExigeValidade);
    }

    [Fact]
    public void ToggleDeValidade_Verdadeiro_Falso()
    {
        var artigo = new Artigo(
        "Garrafa de Água",
         999,
         122,
         ArtigoClasses.A);

        artigo.ToggleValidade();

        Assert.True(artigo.ExigeValidade);
    }
}
