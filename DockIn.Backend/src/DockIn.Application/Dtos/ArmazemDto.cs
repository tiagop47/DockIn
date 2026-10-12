using DockIn.Domain.Entities;

namespace DockIn.Application.Dtos;

public record ArmazemDto(
    int ArmazemId,
    string Nome,
    string? Descricao,
    bool Ativo
);

public record CriarArmazemDto(
    string Nome,
    string? Descricao = null
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
