using DockIn.Application.Dtos;

public class StockService
{
    private readonly IStockRepository _stockRepository;
    private readonly IArtigoRepository _artigoRepository;

    public StockService(IStockRepository repository, IArtigoRepository artigoRepository)
    {
        _stockRepository = repository;
        _artigoRepository = artigoRepository;
    }

    public async Task<StockDto?> CriarStockAsync(CriarStockDto stock)
    {
        if (stock == null)
        {
            throw new ArgumentNullException(nameof(stock), "O Stock é inválido");
        }

        var artigo = await _artigoRepository.ObterArtigoPorIdAsync(stock.ArtigoId);
        if (artigo == null)
        {
            throw new ArgumentNullException(nameof(artigo), "O artigo não existe");
        }

        var stockTmp = new Stock(artigo,
                                 stock.Preco,
                                 stock.Quantidade);

        await _stockRepository.AdicionarStockAsync(stockTmp);

        return new StockDto(
            stockTmp.StockId,
            stockTmp.ArtigoId,
            stockTmp.Preco,
            stockTmp.Quantidade,
            stockTmp.QuantidadeReservada,
            stockTmp.CreatedAt
        );
    }

    public async Task<bool> IncrementarQuantidadeStockAsync(int id, int quantidade)
    {
        Stock? tmp = await _stockRepository.ObterStockPorIdAsync(id);

        if (tmp == null)
        {
            throw new ArgumentNullException(nameof(id), "Não existe nenhum stock com esse id");
        }

        tmp.IncrementarQuantidade(quantidade);
        await _stockRepository.AtualizarStockPorIdAsync(tmp);

        return true;
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


}
