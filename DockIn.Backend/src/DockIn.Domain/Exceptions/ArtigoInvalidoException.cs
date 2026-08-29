using DockIn.Domain;

public class ArtigoInvalidoException : DomainException
{
    public ArtigoInvalidoException(int id) : base($"O artigo {id} não é válido")
    {
    }
}
