using DockIn.Application.Abstractions;
using DockIn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DockIn.Infrastructure.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private ApplicationDbContext _context;

        public LocalizacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AtivarLocalizacaoAsync(Localizacao localizacao)
        {
            localizacao.Ativar();
            await _context.SaveChangesAsync();
        }

        public async Task CriarLocalizacao(Localizacao localizacao)
        {
            await _context.AddAsync(localizacao);
            await _context.SaveChangesAsync();
        }

        public Task DesativarLocalizacaoAsync()
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<Localizacao>> ObterLocalizacaoPaginada(int pagina = 1, int tamanho = 5)
        {
            throw new NotImplementedException();
        }

        public Task<Localizacao?> ObterLocalizacao(int armazemId)
        {
            throw new NotImplementedException();
        }

        public async Task<Localizacao?> ObterLocalizacao(int armazemId, int localizacaoId)
        {
            return await _context.Localizacoes.FirstOrDefaultAsync(u => u.ArmazemId
            == armazemId && u.LocalizacaoId == localizacaoId);
        }

        public Task<Armazem> ObterArmazemLocalizacao(int armazemId)
        {
            throw new NotImplementedException();
        }
    }
}
