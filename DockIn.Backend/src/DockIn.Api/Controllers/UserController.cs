using DockIn.Application.Dtos;
using DockIn.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DockIn.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObterUsers([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var users = await _service.ObterUsers(pagina, tamanho);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterUserPorId(int id)
        {
            var users = await _service.ObterUserPorId(id);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUser([FromBody] CriarUserDto dto)
        {
            var user = await _service.CriarUserAsync(dto);
            return CreatedAtAction(nameof(CriarUser), new { id = user.UserId });
        }

    }
}
