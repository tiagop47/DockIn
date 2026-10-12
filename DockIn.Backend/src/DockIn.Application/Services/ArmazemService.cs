using DockIn.Application.Abstractions;
using DockIn.Application.Dtos;
using DockIn.Domain.Entities;

public class ArmazemService
{
    private readonly IArmazemRepository _armazemRepository;
    private readonly IArtigoRepository _artigoRepository;

    public ArmazemService(IArmazemRepository armazemRepository, IArtigoRepository artigoRepository)
    {
        _armazemRepository = armazemRepository;
        _artigoRepository = artigoRepository;
    }

    public async Task<ArmazemDto> ObterIdAsync(int id)
    {
        if(id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        var armazem = await _armazemRepository.ObterArmazemPorId(id);

        if(armazem == null)
        {
            throw new NotFoundException(nameof(id));
        }

        return armazem.ToDto();
    }

    public async Task<ArmazemDto> CriarArmazemAsync(CriarArmazemDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var objeto = new Armazem(dto.Nome, dto.Descricao);
        await _armazemRepository.CriarArmazem(objeto);

        return objeto.ToDto();
    }


}
