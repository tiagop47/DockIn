using Application.Tests.Integracao;
using DockIn.Application.Dtos;

public class ArtigoIntegracaoTests : ArtigoRepoIntegracao
{
    [Fact]
    public async Task CriarArtigo_BaseDados_Valido()
    {
        var dto = new CriarArtigoDto("Sumol", 5, 5, UnidadeMedida.Unidade);

        var resultado = await _service.CriarArtigoAsync(dto);
        Assert.NotNull(resultado);

        var artigoBaseDados = await _context.Artigos.FindAsync(resultado.ArtigoId);

        Assert.NotNull(artigoBaseDados);
        Assert.Equal("Sumol", artigoBaseDados.Description);
    }
}
