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

}
