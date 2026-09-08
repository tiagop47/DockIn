namespace DockIn.Domain;

public class Artigo
{
    public int ArtigoId { get; }

    public string Description { get; set; } = string.Empty;

    public int QUANTIDADE_MAX { get; private set; } = 100;

    private decimal _peso;
    public decimal Peso
    {
        get => _peso;

        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_peso), "Não pode existir artigo com _peso negativo");
            }

            if (value > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(_peso), "Não é fisicamente possível armazenar");
            }

            _peso = value;
        }
    }

    private decimal _dimensoes;
    public decimal Dimensoes
    {
        get => _dimensoes;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_dimensoes), "Não podem existir dimensões negativas");
            }

            if (value > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(_dimensoes), "Não é fisicamente possível armazenar");
            }

            _dimensoes = value;
        }
    }

    public bool ExigeValidade { get; private set; } = false;


    public DateTime CreatedAt { get; private set; }
    public ArtigoClasses ArtigoClasses { get; set; }

    public Artigo() { }

    internal Artigo(int artigoId, string description,
                  decimal peso,
                  decimal dimensoes,
                  ArtigoClasses artigoClasses) : this(description, peso, dimensoes, artigoClasses)
    {
        ArtigoId = artigoId;
    }

    public Artigo(string description, decimal peso, decimal dimensoes, ArtigoClasses artigoClasses)
    {
        Description = description;
        Peso = peso;
        Dimensoes = dimensoes;
        ArtigoClasses = artigoClasses;
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

    public void AtualizarCapacidadeMax_Artigo(int capacidade)
    {
        if (capacidade <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacidade), "A capacidade máxima tem de ser superior a zero.");
        }

        if (capacidade > 10000)
        {
            throw new ArgumentOutOfRangeException(nameof(capacidade), "A capacidade máxima excede o limite permitido.");
        }

        QUANTIDADE_MAX = capacidade;
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
