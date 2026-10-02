using DockIn.Application.Abstractions;
using DockIn.Application.Dtos;
using DockIn.Application.Mappers;
using DockIn.Domain;

namespace DockIn.Application.Services
{
  public class ArtigoService
  {
    private readonly IArtigoRepository _repository;

    public ArtigoService(IArtigoRepository repository)
    {
      _repository = repository;
    }

    public async Task<ArtigoDto> ObterPorIdAsync(int id)
    {
      if (id < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(id), "Introduz Id's inteiros positivos");
      }

      Artigo? artigo = await _repository.ObterArtigoPorIdAsync(id);
      if (artigo != null)
      {
        return artigo.ToDto();
      }

      throw new ArtigoNaoEncontradoException(id);
    }

    public async Task<ArtigoDto> CriarArtigoAsync(CriarArtigoDto artigo)
    {
      if (artigo == null)
      {
        throw new ArgumentNullException(nameof(artigo), "Artigo é nulo");
      }

      var tmp = new Artigo(artigo.Peso, artigo.Dimensoes,
          artigo.UnidadeMedida, artigo.Description);

      await _repository.AdicionarArtigoAsync(tmp);

      return tmp.ToDto();
    }

    public async Task<bool> RemoverArtigoAsync(int id)
    {
      if (id < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(id), "Introduz Id's inteiros positivos");
      }

      var artigo = await _repository.ObterArtigoPorIdAsync(id);
      if (artigo == null)
      {
        return false;
      }

      await _repository.EliminarArtigoPorIdAsync(id);
      return true;
    }

    public async Task<IEnumerable<ArtigoDto>> ObterArtigosPaginados(int pagina = 1, int tamanho = 5)
    {
      if (pagina <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(pagina), "A página deve ser > 1");
      }

      var artigos = await _repository.ObterArtigoPaginado(pagina, tamanho);

      return artigos.Select(a => a.ToDto());
    }
  }
}
