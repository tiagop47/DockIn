namespace DockIn.Domain;

public class Artigo
{
    public int ArtigoId { get; }
    public string Description { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Dimensoes { get; private set; }
    public UnidadeMedida UnidadeMedida { get; private set; }
    public bool AtivoBool { get; private set; } = false;
    public DateTime CreatedAt { get; private set; }

    public Artigo(decimal peso, decimal dimensoes, UnidadeMedida unidade = UnidadeMedida.Unidade, string description = "")
    {
        ValidarPeso(peso);
        ValidarDimensoes(dimensoes);
        ValidarUnidadeMedida(unidade);
        Description = description;
        Peso = peso;
        Dimensoes = dimensoes;
        UnidadeMedida = unidade;
        CreatedAt = DateTime.UtcNow;
    }

    private Artigo() { Description = null!; }

    internal Artigo(int artigoId, decimal peso, decimal dimensoes, UnidadeMedida unidade, string description = "")
        : this(peso, dimensoes, unidade, description)
    {
        ArtigoId = artigoId;
    }

    public void Ativar()
    {
        AtivoBool = true;
    }

    public void Desativar()
    {
        AtivoBool = false;
    }

    public void AtualizarDimensoes(decimal dimensoes)
    {
        ValidarDimensoes(dimensoes);
        Dimensoes = dimensoes;
    }

    public void AtualizarPeso(decimal peso)
    {
        ValidarPeso(peso);
        Peso = peso;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Artigo outro)
        {
            return false;
        }

        if (ReferenceEquals(this, outro))
        {
            return true;
        }

        if (ArtigoId > 0 && outro.ArtigoId > 0)
        {
            return ArtigoId == outro.ArtigoId;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ArtigoId > 0 ? HashCode.Combine(ArtigoId) : base.GetHashCode();
    }

    private static void ValidarPeso(decimal peso)
    {
        if (peso <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(peso), "O peso deve ser superior a zero.");
        }
    }

    private static void ValidarDimensoes(decimal dimensoes)
    {
        if (dimensoes <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dimensoes), "As dimensões devem ser superiores a zero.");
        }
    }

    private static void ValidarUnidadeMedida(UnidadeMedida unidade)
    {
        if (!Enum.IsDefined(unidade))
        {
            throw new ArgumentOutOfRangeException(nameof(unidade), "Unidade de medida inválida.");
        }
    }
}
