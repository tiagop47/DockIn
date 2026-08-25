using DockIn.Domain;
using Microsoft.EntityFrameworkCore;

public class StockRepository : IStockRepository
{

    ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarStockAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
    }

    public Task DecrementarStockPorIdAsync(int id, int quantidade)
    {
        throw new NotImplementedException();
    }

    public async Task EliminarStockPorIdAsync(int id)
    {
        await _context.Stocks.Where(s => s.StockId == id)
                             .ExecuteDeleteAsync();
    }

    public async Task AtualizarStockPorIdAsync(Stock stock)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Stock>> ObterStockPaginado(int pagina, int tamanho = 20)
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
