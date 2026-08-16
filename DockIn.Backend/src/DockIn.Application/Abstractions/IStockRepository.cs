using DockIn.Domain;

public interface IStockRepository
{
    Task<Stock?> ObterStockPorIdAsync(int id);
    Task AdicionarStockAsync(Stock stock);
    Task EliminarStockPorIdAsync(int id);
    Task AtualizarStockPorIdAsync(Stock stock);
    Task<IEnumerable<Stock>> ObterStockPaginado(int pagina, int tamanhoPagina = 20);
}
