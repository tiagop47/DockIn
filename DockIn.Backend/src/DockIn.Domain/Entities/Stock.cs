using DockIn.Domain;

public class Stock
{
    public int StockId { get; private set; }

    public int ArtigoId { get; private set; }

    public double Preco { get; private set; }

    public int Quantidade { get; private set; }

    public int QuantidadeReservada { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public int Versao { get; private set; } = 1;

    public Stock() { }

    public Stock(Artigo artigo, double preco, int quantidade)
    {
        if (artigo == null)
        {
            throw new ArgumentNullException(nameof(artigo), "O artigo não existe");
        }

        if (quantidade < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "Quantidade tem de ser inteira positiva");
        }

        if (preco < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(preco), "Preco tem de ser positivo");
        }

        ArtigoId = artigo.ArtigoId;
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

        Quantidade += quantidade;
        Versao++;
    }

    public void DecrementarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentOutOfRangeException("Coloque inteiros positivos para representar quantidade");
        }

        int delta = Quantidade - quantidade;
        if (delta < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "Quantidades negativas não sao aceites");
        }

        Quantidade = delta;
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

    public static explicit operator Stock(Task<Stock?> v)
    {
        throw new NotImplementedException();
    }
}
