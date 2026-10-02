
using DockIn.Application.Dtos;
using DockIn.Domain;

namespace DockIn.Application.Mappers
{
  public static class ArtigoMapper
  {
    public static ArtigoDto ToDto(this Artigo artigo)
    {
      return new ArtigoDto(
          artigo.ArtigoId,
          artigo.Description,
          artigo.Peso,
          artigo.Dimensoes,
          artigo.UnidadeMedida,
          artigo.CreatedAt
          );
    }
  }
}
