using DockIn.Domain;
using DockIn.Domain.Entities;

namespace Domain.Tests
{
    public class ArmazemTests
    {
        public static readonly Random random = new Random();

        [Fact]
        public void CriacaoArmazem_Ok()
        {
            Armazem armazem = new Armazem(Localizacao.Porto);

            Assert.True(armazem != null);
        }

        [Fact]
        public void CriacaoStock_Artigo_Nao_Existe()
        {
            var armazem = new Armazem(Localizacao.Porto);
            var artigo = new Artigo(10, 5, 5, UnidadeMedida.Quilograma);

            armazem.AdicionarStock(artigo, 5, 5);
            int StockArmazem = armazem.Stock.Count;

            Assert.Equal(1, StockArmazem);
        }

        [Fact]
        public void AdicionarQuantidade_StockSob_MesmoArtigo()
        {
            Armazem armazem = new Armazem(Localizacao.Porto);
            var artigo = new Artigo(12, 5, 5, UnidadeMedida.Quilograma);

            armazem.AdicionarStock(artigo, 5, 5);
            armazem.AdicionarStock(artigo, 3, 5);

            var stock = armazem.Stock.First();

            var quantidade = 0;
            foreach (var stocks in armazem.Stock)
            {
                if (stocks.ArtigoId == artigo.ArtigoId)
                {
                    quantidade = stocks.Quantidade;
                }
            }

            Assert.Equal(8, quantidade);
            Assert.Single(armazem.Stock);
        }

        [Fact]
        public void RemoverStock_MinimoViavel_ManterSoftDelete()
        {
            Armazem armazem = new Armazem(Localizacao.Porto);
            var artigo = new Artigo(124, 5, 5, UnidadeMedida.Litro);

            armazem.AdicionarStock(artigo, 5, 5);
            armazem.RemoverStock(artigo, 5);

            Stock stock = armazem.Stock.First();

            Assert.Equal(0, stock.Quantidade);
            Assert.Single(armazem.Stock);
        }

        [Fact]
        public void LugaresDisponiveis_CalculoCoerente()
        {
            var armazem = new Armazem(Localizacao.Porto);

            var artigoA = new Artigo(0, 5, 5, UnidadeMedida.Litro);
            var artigoB = new Artigo(1, 5, 5, UnidadeMedida.Quilograma);

            armazem.AdicionarStock(artigoA, 5, 5);
            armazem.AdicionarStock(artigoB, 5, 5);

            int stock = armazem.Stock.Sum(s => s.Quantidade);

            Assert.Equal(stock, armazem.CapacidadeMaxima - armazem.LugaresDisponiveis());
        }

        [Fact]
        public void Limite_CapacidadeExcedido_PorUm()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Armazem(Localizacao.Porto, 1000));
        }

        [Fact]
        public void Limite_Capacidade_Invalido_Negativo()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Armazem(Localizacao.Porto, -1));
        }

        [Fact]
        public void IncrementarStock_Quantidade_InvalidaSuperior()
        {
            var armazem = new Armazem(Localizacao.Porto, 999);
            var artigoA = new Artigo(0, 5, 5, UnidadeMedida.Litro);

            armazem.AdicionarStock(artigoA, 998, 5);
            Stock stock = armazem.Stock.First(s => s.Quantidade > 0);

            Assert.Equal(998, stock.Quantidade);

            Assert.Throws<DomainException>(() => armazem.AdicionarStock(artigoA, 2, 5));
        }
    }
}
