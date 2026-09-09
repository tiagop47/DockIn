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

    [Fact]
    public void CapacidadeMax_artigo_CaiEmDefault()
    {
        var artigo = new Artigo(
        "Garrafa de Água",
         999,
         122,
         ArtigoClasses.A);

        Assert.True(artigo.QUANTIDADE_MAX == 100);
    }

    [Fact]
    public void CapacidadeMax_ArtigoAtualizacao_InvalidaInferior()
    {
        var artigo = new Artigo(
        "Garrafa de Água",
         999,
         122,
         ArtigoClasses.A);

        Assert.Throws<ArgumentOutOfRangeException>(() => artigo.AtualizarCapacidadeMax_Artigo(-500));
    }

    [Fact]
    public void CapacidadeMax_ArtigoAtualizacao_InvalidaSuperior()
    {
        var artigo = new Artigo(
        "Garrafa de Água",
         999,
         122,
         ArtigoClasses.A);

        Assert.Throws<ArgumentOutOfRangeException>(() => artigo.AtualizarCapacidadeMax_Artigo(10001));
    }

    [Fact]
    public void CapacidadeMax_ArtigoAtualizacao_Valida()
    {
        var artigo = new Artigo(
        "Garrafa de Água",
         999,
         122,
         ArtigoClasses.A);


        artigo.AtualizarCapacidadeMax_Artigo(300);
        Assert.Equal(300, artigo.QUANTIDADE_MAX);
    }

}
