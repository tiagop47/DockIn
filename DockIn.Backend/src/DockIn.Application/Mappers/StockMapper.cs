using DockIn.Application.Dtos;

public static class StockMapper
{
  public static StockDto ToDto(this Stock stock)
  {
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
