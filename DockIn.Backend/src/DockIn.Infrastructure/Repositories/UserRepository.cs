using DockIn.Application.Abstractions;
using DockIn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DockIn.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ObterUserEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.Equals(email));
        }

        public async Task CriarUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAlteracoes()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> ObterUserId(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> ObterUsersPaginados(int pagina, int tamanho)
        {
            return await _context.Users.AsNoTracking()
                .OrderBy(u => u.UserId)
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho)
                .ToListAsync();
        }

        public async Task RemoverUser(User user)
        {
            _context.Users.Remove(user);
        }
    }
}