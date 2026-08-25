using DockIn.Domain;

public interface IArtigoRepository
{
    Task<Artigo?> ObterArtigoPorIdAsync(int id);
    Task AdicionarArtigoAsync(Artigo artigo);
    Task EliminarArtigoPorIdAsync(int id);
    Task<IEnumerable<Artigo>> ObterArtigoPaginado(int pagina, int tamanho = 20);
}
