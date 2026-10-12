using DockIn.Application.Abstractions;
using DockIn.Application.Dtos;
using DockIn.Application.Services;
using DockIn.Domain.Entities;
using Moq;

namespace Unitarios.Application.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repository;
    private readonly Mock<IPasswordHasher> _passwordHasher;
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _repository = new Mock<IUserRepository>();
        _passwordHasher = new Mock<IPasswordHasher>();
        _sut = new UserService(_repository.Object, _passwordHasher.Object);
    }

    [Fact]
    public async Task User_PasswordHash_Valida()
    {
        const string password = "tiago";
        var criarUserDto = new CriarUserDto("tiago@gmail.com",
            Perfil.Administrador,
            "tiago");
        var user = await _sut.CriarUserAsync(criarUserDto);

        Console.Write(user.PasswordHash);
        Assert.NotEqual(password, user.PasswordHash);
    }
    
    [Fact]
    public async Task User_PasswordHash_Valida_Mais_5_Carateres()
    {
        const string password = "tiago";
        var criarUserDto = new CriarUserDto("tiago@gmail.com",
            Perfil.Administrador,
            "tiago");
        var user = await _sut.CriarUserAsync(criarUserDto);

        Console.Write(user.PasswordHash);
        Assert.NotEqual(password, user.PasswordHash);
    }
    
   
}