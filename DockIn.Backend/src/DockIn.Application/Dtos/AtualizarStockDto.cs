/// <summary>
///  Mesmo Dto para Incrementar ou Decrementar a quantidade de stock
/// </summary>
/// <param name="stockId"></param>
/// <param name="quantidade"></param>
public record AtualizarStockDto(int stockId, int quantidade);
