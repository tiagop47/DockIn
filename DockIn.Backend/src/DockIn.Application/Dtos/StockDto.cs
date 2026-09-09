namespace DockIn.Application.Dtos;

public record StockDto(
    int StockId,
    int ArtigoId,
    decimal Preco,
    int Quantidade,
    int QuantidadeReservada,
    DateTime CreatedAt
);
