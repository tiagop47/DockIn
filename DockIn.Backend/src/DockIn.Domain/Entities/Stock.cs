using DockIn.Domain;

public class Stock
{
    public int StockId { get; private set; }

    public int ArmazemId { get; private set; }

    public int ArtigoId { get; private set; }

    public decimal _preco;
    public decimal Preco
    {
        get => _preco;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Preco tem de ser positivo");
            }

            _preco = value;
        }
    }

    private int _quantidade;
    public int Quantidade
    {
        get => _quantidade;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_quantidade), "Quantidade tem de ser inteira positiva");
            }

            _quantidade = value;
        }
    }

    public int CapacidadeMaxima { get; private set; }
    public int QuantidadeReservada { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int Versao { get; private set; } = 1;

    public Stock() { }

    public Stock(Armazem armazem, Artigo artigo, decimal preco, int quantidade)
    {
        if (artigo == null)
        {
            throw new ArgumentNullException(nameof(artigo), "O artigo não existe");
        }

        if (armazem == null)
        {
            throw new ArgumentNullException(nameof(artigo), "O artigo não existe");
        }

        if (quantidade > artigo.QUANTIDADE_MAX)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade inicial excede a capacidade máxima.");
        }

        ArmazemId = armazem.ArmazemId;
        ArtigoId = artigo.ArtigoId;
        CapacidadeMaxima = artigo.QUANTIDADE_MAX;
        Preco = preco;
        Quantidade = quantidade;
        CreatedAt = DateTime.UtcNow;
    }

    public void IncrementarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "Coloque inteiros positivos para representar quantidade");
        }

        if (quantidade + Quantidade > CapacidadeMaxima)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "Cap máxima Excedida");
        }

        Quantidade += quantidade;
        Versao++;
    }

    public void DecrementarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentOutOfRangeException("Coloque inteiros positivos para representar quantidade");
        }

        if (Quantidade - quantidade < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "Quantidades negativas não sao aceites");
        }

        Quantidade -= quantidade;
        Versao++;
    }

    // override object.Equals
    public override bool Equals(object? obj)
    {
        if (obj is not Stock outro) return false;

        // Se ambos já têm ID de base de dados gerado, compara pelo StockId
        if (StockId > 0 && outro.StockId > 0)
        {
            return StockId == outro.StockId;
        }

        // Caso contrário (em memória), compara pelas propriedades do stock
        return ArtigoId == outro.ArtigoId && Preco == outro.Preco && Quantidade == outro.Quantidade;

    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return StockId > 0
            ? HashCode.Combine(StockId)
            : HashCode.Combine(ArtigoId, Preco, Quantidade);
    }

    public override string ToString()
    {
        return $"{ArmazemId}, {ArtigoId}";

    }
}
