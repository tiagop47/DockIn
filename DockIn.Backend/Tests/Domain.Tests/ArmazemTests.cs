using DockIn.Domain;
using DockIn.Domain.Entities;

namespace Domain.Tests
{
    public class ArmazemTests
    {
        [Fact]
        public void CriarArmazem_ComNomeValido_DeveInicializarComSucesso()
        {
            var armazem = new Armazem("Armazem Central", "Descricao teste");

            Assert.Equal("Armazem Central", armazem.Nome);
            Assert.Equal("Descricao teste", armazem.Descricao);
            Assert.True(armazem.Ativo);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CriarArmazem_SemNome_DeveLancarDomainException(string? nomeInvalido)
        {
            Assert.Throws<DomainException>(() => new Armazem(nomeInvalido!));
        }

        [Fact]
        public void AlterarDados_Valido_DeveAtualizarPropriedades()
        {
            var armazem = new Armazem("Original");
            armazem.AlterarDados("Atualizado", "Nova descricao");

            Assert.Equal("Atualizado", armazem.Nome);
            Assert.Equal("Nova descricao", armazem.Descricao);
        }

        [Fact]
        public void InativarEAtivar_DeveAlterarEstado()
        {
            var armazem = new Armazem("Principal");
            Assert.True(armazem.Ativo);

            armazem.Inativar();
            Assert.False(armazem.Ativo);

            armazem.Ativar();
            Assert.True(armazem.Ativo);
        }
    }
}
