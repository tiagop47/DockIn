using DockIn.Application.Abstractions;
using DockIn.Application.Services;
using DockIn.Infrastructure.Auth;
using DockIn.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DockIn.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            //Repositórios
            services.AddScoped<IArtigoRepository, ArtigoRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IArmazemRepository, ArmazemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //Serviços
            services.AddScoped<ArtigoService>();
            services.AddScoped<StockService>();
            services.AddScoped<UserService>();
            services.AddScoped<ArmazemService>();
            return services;
        }
    }
}