using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
          options.UseNpgsql(connectionString));

        //Repositórios
        services.AddScoped<IArtigoRepository, ArtigoRepository>();
        services.AddScoped<IStockRepository, StockRepository>();

        //Serviços
        services.AddScoped<ArtigoService>();
        services.AddScoped<StockService>();
        return services;
    }
}
