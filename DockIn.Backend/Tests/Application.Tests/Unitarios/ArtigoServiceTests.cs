namespace Unitarios.Application.Tests;

using DockIn.Application.Dtos;
using DockIn.Domain;
using Moq;
using DockIn.Application.Abstractions;
using DockIn.Application.Services;

public class ArtigoServiceTests
{
    private readonly Mock<IArtigoRepository> _repositoryMock;
    private readonly ArtigoService _sut;

    public ArtigoServiceTests()
    {
        _repositoryMock = new Mock<IArtigoRepository>();
        _sut = new ArtigoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task ObterPorId_Invalido_NaoDeveIr_RepositoryAsync()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.ObterPorIdAsync(-1));
        _repositoryMock.Verify(r => r.ObterArtigoPorIdAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task ObterPorId_Valido_DeveRetornarObjeto()
    {
        var artigo = new Artigo(1, 5m, 5m, UnidadeMedida.Unidade, "Sumol");
        _repositoryMock.Setup(r => r.ObterArtigoPorIdAsync(1)).ReturnsAsync(artigo);

        var dto = await _sut.ObterPorIdAsync(1);

        Assert.Equal(1, dto.ArtigoId);
        Assert.Equal("Sumol", dto.Description);
        Assert.Equal(UnidadeMedida.Unidade, dto.UnidadeMedida);
    }

    [Fact]
    public async Task CriarArtigo_Invalido_NaoTocaRepository()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.CriarArtigoAsync(null!));
        _repositoryMock.Verify(r => r.AdicionarArtigoAsync(It.IsAny<Artigo>()), Times.Never);
    }

    [Fact]
    public async Task CriarArtigo_Valido()
    {
        var dto = new CriarArtigoDto("Sumol", 5, 5, UnidadeMedida.Unidade);

        var artigoDto = await _sut.CriarArtigoAsync(dto);

        Assert.Equal("Sumol", artigoDto.Description);
        Assert.Equal(5, artigoDto.Peso);
        Assert.Equal(UnidadeMedida.Unidade, artigoDto.UnidadeMedida);

        _repositoryMock.Verify(r => r.AdicionarArtigoAsync(It.IsAny<Artigo>()), Times.Once);
    }

    [Fact]
    public async Task RemoverArtigo_Invalido()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.RemoverArtigoAsync(-1));
        _repositoryMock.Verify(r => r.EliminarArtigoPorIdAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Tentativa_RemoverId_ValidoInexistente()
    {
        _repositoryMock.Setup(r => r.ObterArtigoPorIdAsync(1)).ReturnsAsync((Artigo?)null);

        bool resultado = await _sut.RemoverArtigoAsync(1);
        Assert.False(resultado);

        _repositoryMock.Verify(r => r.ObterArtigoPorIdAsync(1), Times.Once);
        _repositoryMock.Verify(r => r.EliminarArtigoPorIdAsync(1), Times.Never);
    }

    [Fact]
    public async Task Tentativa_RemoverId_ValidoExistente()
    {
        var artigo = new Artigo(1, 5m, 5m, UnidadeMedida.Unidade, "Sumol");
        _repositoryMock.Setup(r => r.ObterArtigoPorIdAsync(1)).ReturnsAsync(artigo);

        bool resultado = await _sut.RemoverArtigoAsync(1);
        Assert.True(resultado);

        _repositoryMock.Verify(r => r.ObterArtigoPorIdAsync(1), Times.Once);
        _repositoryMock.Verify(r => r.EliminarArtigoPorIdAsync(1), Times.Once);
    }
}
