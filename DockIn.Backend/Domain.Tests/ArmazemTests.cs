public class ArmazemTests
{
    [Fact]
    public void CriacaoArmazem_Ok_1()
    {
        Armazem armazem = new Armazem(Localizacao.Porto);

        Assert.True(armazem != null);
    }
}
