namespace DockIn.Infrastructure.Repositories;

using System.Runtime.CompilerServices;
using DockIn.Application.Abstractions;
using DockIn.Application.Services;
using DockIn.Domain;

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

    public Task EliminarArtigoPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Artigo>> ObterArtigoPaginado(int pagina, int tamanho = 20)
    {
        throw new NotImplementedException();
    }

    public async Task<Artigo?> ObterArtigoPorIdAsync(int id)
    {
        return await _context.Artigos.FindAsync(id);
    }
}
