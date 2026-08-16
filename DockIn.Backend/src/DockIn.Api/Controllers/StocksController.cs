using DockIn.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock")]
public class StocksController : ControllerBase
{
    StockService _service;

    public StocksController(StockService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        try
        {
            StockDto? stock = await _service.ObterStockPorIdAsync(id);
            return Ok(stock);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarStock([FromBody] CriarStockDto dto)
    {
        try
        {
            StockDto? stock = await _service.CriarStockAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = stock.StockId }, stock);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
