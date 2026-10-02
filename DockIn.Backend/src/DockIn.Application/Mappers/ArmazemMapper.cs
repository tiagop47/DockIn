using DockIn.Application.Dtos;
using DockIn.Domain.Entities;

public static class ArmazemMapper
{
    public static ArmazemDto ToDto(this Armazem armazem)
    {
        return new ArmazemDto(
          armazem.ArmazemId,
          armazem.Localizacao,
          armazem.LugaresDisponiveis(),
          armazem.Stock.Select(s => s.ToDto())
                       .ToList()
          );

    }
}

