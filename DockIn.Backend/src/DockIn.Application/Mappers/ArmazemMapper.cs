using DockIn.Application.Dtos;
using DockIn.Domain.Entities;

public static class ArmazemMapper
{
    public static ArmazemDto ToDto(this Armazem armazem)
    {
        return new ArmazemDto(
            armazem.ArmazemId,
            armazem.Nome,
            armazem.Descricao,
            armazem.Ativo
        );
    }

    public static Armazem ToEntity(this CriarArmazemDto dto)
    {
        return new Armazem(dto.Nome, dto.Descricao);
    }

    public static Armazem ToEntity(this ArmazemDto dto)
    {
        return new Armazem(dto.Nome, dto.Descricao);
    }
}

