using Microsoft.EntityFrameworkCore;

public class ArmazemRepository : IArmazemRepository
{
    ApplicationDbContext _context;

    public ArmazemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CriarArmazem(Armazem armazem)
    {
        await _context.Armazens.AddAsync(armazem);
        await _context.SaveChangesAsync();
    }

    public void RemoverArmazem(Armazem armazem)
    {
        _context.Armazens.Remove(armazem);
    }

    public async Task GuardarAlteracoes()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Armazem?> ObterArmazemPorId(int id)
    {
        return await _context.Armazens.Include(a => a.Stock)
                                      .FirstOrDefaultAsync(a => a.ArmazemId == id);

    }

    public async Task<IEnumerable<Armazem>?> ObterArmazemPaginado(int pagina, int tamanho = 5)
    {
        return await _context.Armazens
        .AsNoTracking()
        .OrderBy(s => s.ArmazemId)
        .Skip((pagina - 1) * tamanho)
        .Take(tamanho)
        .ToListAsync();
    }
}
