using System.Net.Http.Headers;
using DockIn.Application.Dtos;

public class ArmazemService
{
    private readonly IArmazemRepository _armazemRepository;
    private readonly IArtigoRepository _artigoRepository;

    public ArmazemService(IArmazemRepository armazemRepository, IArtigoRepository artigoRepository)
    {
        _armazemRepository = armazemRepository;
        _artigoRepository = artigoRepository;
    }

    public async Task<ArmazemDto> ObterIdAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        var armazem = await _armazemRepository.ObterArmazemPorId(id);
        if (armazem == null)
        {
            throw new NotFoundException(nameof(id));
        }

        return new ArmazemDto(
            armazem.ArmazemId,
            armazem.Localizacao,
            armazem.CapacidadeMaxima,
            armazem.OcupacaoArmazem(),
            armazem.LugaresDisponiveis(),
            armazem.Stock.Select(s => new StockDto(
                s.StockId,
                s.ArtigoId,
                s.Preco,
                s.Quantidade,
                s.QuantidadeReservada,
                s.CreatedAt)).ToList());
    }

    public async Task<ArmazemDto> CriarArmazemAsync(CriarArmazemDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var objeto = new Armazem(dto.Localizacao, dto.Capacidade);
        await _armazemRepository.CriarArmazem(objeto);

        return new ArmazemDto(objeto.ArmazemId
                            , objeto.Localizacao
                            , objeto.CapacidadeMaxima
                            , objeto.OcupacaoArmazem()
                            , objeto.LugaresDisponiveis()
                            , objeto.Stock.Select(s => new StockDto(s.StockId,
                                                    s.ArtigoId,
                                                    s.Preco,
                                                    s.Quantidade,
                                                    s.QuantidadeReservada,
                                                    s.CreatedAt)).ToList());
    }

    public async Task<StockDto> AdicionarStockAsync(int armazemId, AdicionarStockDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (armazemId <= 0)
            throw new ArgumentOutOfRangeException(nameof(armazemId));

        if (dto.Quantidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(dto.Quantidade));

        if (dto.Preco < 0)
            throw new ArgumentOutOfRangeException(nameof(dto.Preco));

        var armazem = await _armazemRepository.ObterArmazemPorId(armazemId)
            ?? throw new NotFoundException(nameof(armazemId));

        var artigo = await _artigoRepository.ObterArtigoPorIdAsync(dto.ArtigoId)
            ?? throw new NotFoundException(nameof(dto.ArtigoId));

        var stock = armazem.AdicionarStock(artigo, dto.Quantidade, dto.Preco);
        await _armazemRepository.GuardarAlteracoes();

        return ToDto(stock);
    }

    public async Task<StockDto> RemoverStockAsync(int armazemId, RemoverStockDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (armazemId <= 0)
            throw new ArgumentOutOfRangeException(nameof(armazemId));

        if (dto.Quantidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(dto.Quantidade));

        var armazem = await _armazemRepository.ObterArmazemPorId(armazemId)
            ?? throw new NotFoundException(nameof(armazemId));

        var artigo = await _artigoRepository.ObterArtigoPorIdAsync(dto.ArtigoId)
            ?? throw new NotFoundException(nameof(dto.ArtigoId));

        var stock = armazem.RemoverStock(artigo, dto.Quantidade);
        await _armazemRepository.GuardarAlteracoes();

        return ToDto(stock);
    }

    private static StockDto ToDto(Stock stock) => new(
        stock.StockId,
        stock.ArtigoId,
        stock.Preco,
        stock.Quantidade,
        stock.QuantidadeReservada,
        stock.CreatedAt);
}
