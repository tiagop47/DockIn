using DockIn.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Criar_User_Invalido_1_Domain()
    {
        Assert.Throws<DomainException>(() => new User("tiago", Perfil.Operador, "teste"));
    }

    [Fact]
    public void Criar_User_EmailNull()
    {
        Assert.Throws<DomainException>(() =>
            new User(null!, Perfil.Administrador, "tiago"));
    }

    [Fact]
    public void CriarUser_PalavraPasse_Invalida()
    {
        Assert.Throws<DomainException>(() => new User(0, "tiago@mail.com", Perfil.Operador, null));
    }

    [Fact]
    public void CriarUser_Valido_DeveInicializarPropriedades()
    {
        var user = new User(1, "ola@mail.com", Perfil.Operador, "teste");

        Assert.Equal(1, user.UserId);
        Assert.Equal("ola@mail.com", user.Email);
        Assert.Equal(Perfil.Operador, user.PerfilUser);
        Assert.Equal("teste", user.PasswordHash);
    }

    [Fact]
    public void CriarUser_SemEmail_DeveLancarDomainExcecao()
    {
        Assert.Throws<DomainException>(() => new User(null, Perfil.Operador, "tiago"));
    }

    [Fact]
    public void CriarUser_SemEmail_DeveLancarDomainExcecao_Carateres()
    {
        Assert.Throws<DomainException>(() => new User("tiago.email", Perfil.Operador, "tiago"));
    }

    [Fact]
    public void CriarUser_SemEmail_DeveLancarDomainExcecao_Carateres_Especial()
    {
        Assert.Throws<DomainException>(() => new User("tiago@email", Perfil.Operador, "tiago"));
    }


}
