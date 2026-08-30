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

}
