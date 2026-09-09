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
        try
        {
            ArmazemDto armazemDto = await _service.ObterIdAsync(id);
            return Ok(armazemDto);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarArmazem([FromBody] CriarArmazemDto dto)
    {
        ArmazemDto armazemDto = await _service.CriarArmazemAsync(dto);
        return CreatedAtAction(nameof(ObterArmazemPorId), new { id = armazemDto.ArmazemId });
    }

    [HttpPost("{armazemId:int}/entrada-stock")]
    public async Task<IActionResult> AdicionarStockAsync(
        int armazemId,
        [FromBody] AdicionarStockDto dto)
    {
        var stock = await _service.AdicionarStockAsync(armazemId, dto);
        return Ok(stock);
    }

    [HttpPost("{armazemId:int}/saida-stock")]
    public async Task<IActionResult> RemoverStockAsync(
        int armazemId,
        [FromBody] RemoverStockDto dto)
    {
        var stock = await _service.RemoverStockAsync(armazemId, dto);
        return Ok(stock);
    }
}
