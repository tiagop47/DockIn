using DockIn.Domain;

public interface IStockRepository
{
    Task<Stock?> ObterStockPorIdAsync(int id);
    Task<IEnumerable<Stock>> ObterStockPaginado(int pagina, int tamanho = 20);
}
