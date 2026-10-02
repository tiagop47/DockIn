namespace DockIn.Application.Dtos;

public record CriarArtigoDto(
    string Description,
    decimal Peso,
    decimal Dimensoes,
    UnidadeMedida UnidadeMedida
);
