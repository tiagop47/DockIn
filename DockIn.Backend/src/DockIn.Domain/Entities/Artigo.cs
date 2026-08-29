namespace DockIn.Domain;

public class Artigo
{
    public int ArtigoId { get; }

    public string Description { get; set; } = string.Empty;

    public decimal Peso { get; private set; } = 0;

    public decimal Dimensoes { get; private set; } = 0;

    public bool ExigeValidade { get; private set; } = false;

    public ArtigoClasses ClasseArtigo { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Artigo() { }

    public Artigo(string description,
                  decimal peso,
                  decimal dimensoes,
                  ArtigoClasses artigoClasses)
    {
        Description = description;
        Peso = SetPeso(peso);
        Dimensoes = SetDimensoes(dimensoes);
        ClasseArtigo = artigoClasses;
        CreatedAt = DateTime.UtcNow;
    }


    public void ToggleValidade()
    {
        if (ExigeValidade)
        {
            ExigeValidade = false;
            return;
        }

        ExigeValidade = true;
    }

    public void DefinirClasseArtigo(ArtigoClasses novaClasse)
    {
        ClasseArtigo = novaClasse;
    }

    public decimal SetPeso(decimal peso)
    {
        if (peso < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(peso), "Não pode existir artigo com peso negativo");
        }

        if (peso > 9999)
        {
            throw new ArgumentOutOfRangeException(nameof(peso), "Não é fisicamente possível armazenar");
        }


        return peso;
    }

    public decimal SetDimensoes(decimal dimensoes)
    {
        if (dimensoes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dimensoes), "Não podem existir dimensões negativas");
        }

        if (dimensoes > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(dimensoes), "Não é fisicamente possível armazenar");
        }


        return dimensoes;
    }

    // override object.Equals
    public override bool Equals(object? obj)
    {
        if (obj is not Artigo outro) return false;

        if (ArtigoId > 0 && outro.ArtigoId > 0)
        {
            return ArtigoId == outro.ArtigoId;
        }

        return string.Equals(Description, outro.Description, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return ArtigoId > 0
            ? HashCode.Combine(ArtigoId)
            : HashCode.Combine(Description?.ToLowerInvariant());
    }


    public override string ToString()
    {
        return $"{Description}, {ArtigoId}";
    }


}
