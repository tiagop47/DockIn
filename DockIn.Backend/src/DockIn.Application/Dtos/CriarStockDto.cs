namespace DockIn.Application.Dtos;

public record CriarStockDto(
    int ArtigoId,
    double Preco,
    int Quantidade
);
