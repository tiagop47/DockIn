using DockIn.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/artigos")]
public class ArtigoController : ControllerBase
{
    private readonly ArtigoService _service;

    public ArtigoController(ArtigoService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterArtigoPorId(int id)
    {
        try
        {
            ArtigoDto artigoDto = await _service.ObterPorIdAsync(id);
            return Ok(artigoDto);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarArtigo([FromBody] CriarArtigoDto dto)
    {
        try
        {
            ArtigoDto? artigoCriado = await _service.CriarArtigoAsync(dto);
            return CreatedAtAction(nameof(ObterArtigoPorId), new { id = artigoCriado.ArtigoId }, artigoCriado);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { mensagem = ex.Message });

        }
    }

    [HttpGet]
    public async Task<IActionResult> ObterArtigosPaginados([FromQuery] int pagina = 1, [FromQuery] int tamanho = 5)
    {
        var artigos = await _service.ObterArtigosPaginados(pagina, tamanho);
        return Ok(artigos);
    }
}
