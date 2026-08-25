using DockIn.Domain;

public class ArtigoInvalidoException : DomainException
{
    public ArtigoInvalidoException(Artigo artigo) : base($"O artigo {artigo} nâo é válido")
    {
    }
}
