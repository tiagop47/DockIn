using DockIn.Domain;
using DockIn.Domain.Entities;
using DockIn.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Artigo> Artigos { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Armazem> Armazens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Localizacao> Localizacoes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ArtigoConfig());
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new ArmazemConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new LocalizacaoConfig());
    }
}