public class ArtigoNaoEncontradoException : NotFoundException
{

    public ArtigoNaoEncontradoException(int id) :
    base($"O artigo com {id} não foi encontrado")
    {
    }
}
