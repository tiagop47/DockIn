using System;
using System.Collections.Generic;
using System.Text;

namespace DockIn.Application.Dtos
{
    public record LocalizacaoDto(int localizacaoId,
                                 int armazemId,
                                 string codigo,
                                 string tipo,
                                 bool ativo);

    public record CriarLocalizacaoDto(int armazemId,
                                      string codigo,
                                      string tipo);
}
