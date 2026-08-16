namespace DockIn.Domain;

public class AnaliseAbcDomainService
{
    public void CalcularEAtribuir(IEnumerable<(Artigo Artigo, int QuantidadeVendida, decimal PrecoUnitario)> dadosVendas, RegraClassificacaoABC? regra = null)
    {
        if (dadosVendas == null)
            throw new ArgumentNullException(nameof(dadosVendas));

        regra ??= RegraClassificacaoABC.Padrao;

        var lista = dadosVendas.Select(d => new
        {
            d.Artigo,
            ValorTotalVendas = d.QuantidadeVendida * d.PrecoUnitario
        }).ToList();

        if (!lista.Any()) return;

        decimal faturacaoTotal = lista.Sum(x => x.ValorTotalVendas);

        if (faturacaoTotal == 0)
        {
            foreach (var item in lista)
                item.Artigo.DefinirClasseArtigo(ArtigoClasses.C);
            return;
        }

        var listaOrdenada = lista
            .OrderByDescending(x => x.ValorTotalVendas)
            .ThenBy(x => x.Artigo.Description)
            .ToList();

        decimal valorAcumulado = 0m;

        foreach (var item in listaOrdenada)
        {
            if (item.ValorTotalVendas == 0)
            {
                item.Artigo.DefinirClasseArtigo(ArtigoClasses.C);
                continue;
            }

            valorAcumulado += item.ValorTotalVendas;
            decimal percentagemAcumulada = (valorAcumulado / faturacaoTotal) * 100m;

            if (percentagemAcumulada <= regra.LimiteClasseA)
            {
                item.Artigo.DefinirClasseArtigo(ArtigoClasses.A);
            }
            else if (percentagemAcumulada <= regra.LimiteClasseB)
            {
                item.Artigo.DefinirClasseArtigo(ArtigoClasses.B);
            }
            else
            {
                item.Artigo.DefinirClasseArtigo(ArtigoClasses.C);
            }
        }
    }
}
