namespace DockIn.Application.Dtos;

public record StockDto(
    int StockId,
    int ArtigoId,
    double Preco,
    int Quantidade,
    int QuantidadeReservada,
    DateTime CreatedAt
);
