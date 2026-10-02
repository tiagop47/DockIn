using DockIn.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Criar_User_Invalido_1_Domain()
    {
        Assert.Throws<DomainException>(() => new User("tiago", Role.GUEST, "teste"));
    }

    [Fact]
    public void Criar_User_EmailNull()
    {
        Assert.Throws<DomainException>(() =>
            new User(null!, Role.USER, "tiago"));
    }

    [Fact]
    public void CriarUser_PalavraPasse_Invalida()
    {
        Assert.Throws<DomainException>(() => new User(0, "tiago@mail.com", Role.USER, "tiago"));
    }

    [Fact]
    public void CriarUser_Valido_DeveInicializarPropriedades()
    {
        var user = new User(1, "ola@mail.com", Role.ADMIN, "teste");

        Assert.Equal(1, user.UserId);
        Assert.Equal("ola@mail.com", user.Email);
        Assert.Equal(Role.ADMIN, user.Role);
        Assert.Equal("teste", user.PasswordHash);
    }

    [Fact]
    public void CriarUser_SemEmail_DeveLancarDomainExcecao()
    {
        Assert.Throws<DomainException>(() => new User(null, Role.ADMIN, "tiago"));
        ;
    }
}
