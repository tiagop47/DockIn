namespace DockIn.Application.Dtos;

public record ArtigoDto(
    int ArtigoId,
    string Description,
    decimal Peso,
    decimal Dimensoes,
    ArtigoClasses ArtigoClasses,
    DateTime CreatedAt
);
