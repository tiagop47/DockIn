using System.Data.Common;
using DockIn.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Integracao;

public class ArtigoRepoIntegracao : IDisposable
{
    private readonly DbConnection _connection;
    protected readonly ApplicationDbContext _context;
    protected readonly IArtigoRepository _repository;
    protected readonly ArtigoService _service;

    public ArtigoRepoIntegracao()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();

        _repository = new ArtigoRepository(_context);
        _service = new ArtigoService(_repository);
    }

    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}
