using DockIn.Domain;
using Domain.Tests;

public class ArmazemTests
{
    private static Artigo[] CriarArtigos()
    {
        Artigo[] artigos = new Artigo[5];

        for (int i = 0; i < artigos.Length; i++)
        {
            artigos[i] = new Artigo($"Artigo {i}"
            , 12
            , 12
            , ArtigoClasses.A);

            if (i == 3) artigos[i].Description = "Coca Cola";
        }

        return artigos;
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

        Artigo[] teste = CriarArtigos();

        for (int i = 0; i < teste.Length; i++)
        {
            armazem.AdicionarStock(teste[i], 5, 10);
        }

        Assert.Equal(5, armazem.OcupacaoArmazem());
    }

    [Fact]
    public void RemoverStock()
    {

        Armazem armazem = new Armazem(Localizacao.Porto);

        Artigo[] teste = CriarArtigos();

        for (int i = 0; i < teste.Length; i++)
        {
            armazem.AdicionarStock(teste[i], 5, 10);
        }

        Artigo temporaria = teste.First(s => s.Description.Equals("Coca Cola"));


        armazem.RemoverStock(temporaria, 1);
        Assert.Equal(temporaria, armazem.);
    }

}
