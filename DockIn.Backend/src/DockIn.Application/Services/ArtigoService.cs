using DockIn.Application.Dtos;
using DockIn.Domain;

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
        if (artigo == null)
        {
            throw new ArtigoNaoEncontradoException(id);
        }

        return new ArtigoDto(
            artigo.ArtigoId,
            artigo.Description,
            artigo.Peso,
            artigo.Dimensoes,
            artigo.ClasseArtigo,
            artigo.CreatedAt
        );
    }

    public async Task<ArtigoDto?> CriarArtigoAsync(CriarArtigoDto artigo)
    {
        if (artigo == null)
        {
            throw new ArgumentNullException(nameof(artigo), "Artigo é nulo");
        }

        var tmp = new Artigo(artigo.Description,
            artigo.Peso,
            artigo.Dimensoes,
            artigo.ClasseArtigo);

        await _repository.AdicionarArtigoAsync(tmp);

        return new ArtigoDto(
            tmp.ArtigoId,
            tmp.Description,
            tmp.Peso,
            tmp.Dimensoes,
            tmp.ClasseArtigo,
            tmp.CreatedAt);
    }

    public async Task<bool> RemoverArtigoAsync(int id)
    {
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
            throw new ArgumentOutOfRangeException(nameof(pagina), "A página deve ser > 0");
        }

        var artigos = await _repository.ObterArtigoPaginado(pagina, tamanho);

        return artigos.Select(a => new ArtigoDto(
            a.ArtigoId,
            a.Description,
            a.Peso,
            a.Dimensoes,
            a.ClasseArtigo,
            a.CreatedAt
        ));
    }
}
