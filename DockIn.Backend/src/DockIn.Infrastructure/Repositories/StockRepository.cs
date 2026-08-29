using DockIn.Domain;
using Microsoft.EntityFrameworkCore;

public class StockRepository : IStockRepository
{

    ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Stock>> ObterStockPaginado(int pagina, int tamanho = 5)
    {
        return await _context.Stocks.AsNoTracking()
        .OrderBy(a => a.StockId)
        .Skip((pagina - 1) * tamanho)
        .Take(tamanho)
        .ToListAsync();
    }

    public async Task<Stock?> ObterStockPorIdAsync(int id)
    {
        var stock = await _context.Stocks.FindAsync(id);
        return stock;
    }

}
