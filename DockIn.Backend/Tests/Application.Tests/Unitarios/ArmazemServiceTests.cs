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
        var objeto = new Armazem(0, Localizacao.Porto, 100);
        _repository.Setup(s => s.ObterArmazemPorId(0)).ReturnsAsync(objeto);

        var ArmazemValido = await _sut.ObterIdAsync(0);

        Assert.Equal(0, ArmazemValido.ArmazemId);
        Assert.Equal(Localizacao.Porto, ArmazemValido.Localizacao);
    }
}
