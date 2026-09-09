namespace DockIn.Application.Dtos;

public record ArmazemDto(
    int ArmazemId,
    Localizacao Localizacao,
    int CapacidadeMaxima,
    int Ocupacao,
    int LugaresDisponiveis,
    IReadOnlyCollection<StockDto> Stocks
);

public record CriarArmazemDto(
    Localizacao Localizacao,
    int Capacidade = 10
);

public record AdicionarStockDto(
    int ArtigoId,
    decimal Preco,
    int Quantidade
);

public record RemoverStockDto(
    int ArtigoId,
    int Quantidade
);
