using DockIn.Application.Dtos;

public class StockService
{
    private readonly IStockRepository _stockRepository;

    public StockService(IStockRepository repository)
    {
        _stockRepository = repository;
    }

    public async Task<StockDto?> ObterStockPorIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentNullException(nameof(id), "Nenhum stock encontrado");
        }

        Stock? stock = await _stockRepository.ObterStockPorIdAsync(id);

        if (stock == null)
        {
            throw new KeyNotFoundException($"O id: {id} não existe");
        }

        return new StockDto(
            stock.StockId,
            stock.ArtigoId,
            stock.Preco,
            stock.Quantidade,
            stock.QuantidadeReservada,
            stock.CreatedAt
        );
    }

    public async Task<IEnumerable<StockDto>?> ObterStockPaginado(int pagina, int tamanho = 5)
    {
        if (pagina < 0)
        {
            throw new ArgumentOutOfRangeException("Número de página Inválido");
        }

        IEnumerable<Stock> stock = await _stockRepository.ObterStockPaginado(pagina, tamanho);

        return stock.Select(s => new StockDto(s.StockId,
            s.ArtigoId,
            s.Preco,
            s.Quantidade,
            s.QuantidadeReservada,
            s.CreatedAt));
    }

}
