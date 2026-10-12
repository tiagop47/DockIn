using DockIn.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/armazens")]
public class ArmazemController : ControllerBase
{
    ArmazemService _service;

    public ArmazemController(ArmazemService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterArmazemPorId(int id)
    {
        ArmazemDto armazemDto = await _service.ObterIdAsync(id);
        return Ok(armazemDto);
    }

    [HttpPost]
    public async Task<IActionResult> CriarArmazem([FromBody] CriarArmazemDto dto)
    {
        ArmazemDto armazemDto = await _service.CriarArmazemAsync(dto);

        return CreatedAtAction(nameof(ObterArmazemPorId), new { id = armazemDto.ArmazemId }, armazemDto);
    }
}
