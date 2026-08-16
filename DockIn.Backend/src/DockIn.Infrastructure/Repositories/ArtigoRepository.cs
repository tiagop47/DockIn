using System.Runtime.CompilerServices;
using DockIn.Domain;
using Microsoft.EntityFrameworkCore;

public class ArtigoRepository : IArtigoRepository
{
    ApplicationDbContext _context;

    public ArtigoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarArtigoAsync(Artigo artigo)
    {
        await _context.Artigos.AddAsync(artigo);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarArtigoPorIdAsync(int id)
    {
        await _context.Artigos.Where(a => a.ArtigoId == id)
                              .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Artigo>> ObterArtigoPaginado(int pagina, int tamanhoPagina = 20)
    {
        return await _context.Artigos.AsNoTracking()
        .OrderBy(a => a.ArtigoId)
        .Skip((pagina - 1) * tamanhoPagina)
        .Take(tamanhoPagina)
        .ToListAsync();
    }

    public async Task<Artigo?> ObterArtigoPorIdAsync(int id)
    {
        return await _context.Artigos.FindAsync(id);
    }

}
