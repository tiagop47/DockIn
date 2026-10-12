using DockIn.Application.Abstractions;
using DockIn.Domain.Entities;
using Moq;

public class ArmazemServiceTests
{
    private readonly Mock<IArmazemRepository> _repository;
    private readonly Mock<IArtigoRepository> _artigoRepo;
    private readonly ArmazemService _sut;

    public ArmazemServiceTests()
    {
        _repository = new Mock<IArmazemRepository>();
        _artigoRepo = new Mock<IArtigoRepository>();
        _sut = new ArmazemService(_repository.Object, _artigoRepo.Object);
    }

    [Fact]
    public async Task ObterArmazem_PorIdValido()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.ObterIdAsync(-1));
        _repository.Verify(a => a.ObterArmazemPorId(-1), Times.Never);
    }

    [Fact]
    public async Task ObterArmazem_PorId_ArmazemValido()
    {
        var objeto = new Armazem(1, "Porto", "Armazem Central", true);
        _repository.Setup(s => s.ObterArmazemPorId(1)).ReturnsAsync(objeto);

        var armazemValido = await _sut.ObterIdAsync(1);

        Assert.Equal(1, armazemValido.ArmazemId);
        Assert.Equal("Porto", armazemValido.Nome);
        Assert.Equal("Armazem Central", armazemValido.Descricao);
        Assert.True(armazemValido.Ativo);
    }
}
