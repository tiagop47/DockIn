using System;
using System.Collections.Generic;
using System.Text;
using DockIn.Domain.Entities;

namespace DockIn.Application.Abstractions
{
    public interface ILocalizacaoRepository
    {
        Task<Localizacao?> ObterLocalizacao(int armazemId, int localizacaoId);
        Task<Armazem> ObterArmazemLocalizacao(int armazemId);
        Task<IEnumerable<Localizacao>> ObterLocalizacaoPaginada(int pagina = 1, int tamanho = 5);
        Task CriarLocalizacao(Localizacao localizacao);
        Task AtivarLocalizacaoAsync(Localizacao localizacao);
        Task DesativarLocalizacaoAsync();
    }
}
