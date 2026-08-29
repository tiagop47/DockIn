public class ArtigoNaoEncontradoException : DomainException
{

    public ArtigoNaoEncontradoException(int id) :
    base($"O artigo com {id} não foi encontrado")
    {
    }
}
