using DockIn.Application.Dtos;
using DockIn.Domain.Entities;

namespace DockIn.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> ObterUserId(int id);
        Task<bool> ObterUserEmail(string email);
        Task CriarUser(User user);
        Task RemoverUser(User user);
        Task GuardarAlteracoes();
        Task<IEnumerable<User>> ObterUsersPaginados(int pagina = 1, int tamanho = 10);
    }
}