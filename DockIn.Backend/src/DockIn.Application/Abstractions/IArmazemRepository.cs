using DockIn.Domain;

public interface IArmazemRepository
{
    Task<Armazem?> ObterArmazemPorId(int id);
    Task<IEnumerable<Armazem>?> ObterArmazemPaginado(int pagina, int tamanho = 5);
    Task CriarArmazem(Armazem armazem);
    void RemoverArmazem(Armazem armazem);
    Task GuardarAlteracoes();
}
