using DockIn.Application.Dtos;
using DockIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DockIn.Application.Mappers
{
    public static class LocalizacaoMapper
    {
        public static LocalizacaoDto ToDto(Localizacao localizacao)
        {
            return new LocalizacaoDto(localizacao.LocalizacaoId,
                                      localizacao.ArmazemId,
                                      localizacao.Codigo,
                                      localizacao.Tipo,
                                      localizacao.Ativo);
        }

        public static Localizacao ToEntity(LocalizacaoDto localizacaoDto)
        {
            return new Localizacao(localizacaoDto.armazemId,
                                   localizacaoDto.codigo,
                                   localizacaoDto.tipo);
        }
    }
}
