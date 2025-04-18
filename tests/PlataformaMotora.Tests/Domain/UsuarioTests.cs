using FluentAssertions;

using PlataformaMotora.Domain.Entities;

namespace PlataformaMotora.Tests.Domain
{
    public class UsuarioTests
    {
        [Fact]
        public void Criar_DeveInstanciarUsuarioCorretamente()
        {
            // Arrange
            var nome = "João da Silva";
            var email = "joao@teste.com";
            var senha = "senha123";

            // Act
            var usuario = Usuario.Criar(nome, email, senha);

            // Assert
            usuario.Should().NotBeNull();
            usuario.Id.Should().NotBe(Guid.Empty);
            usuario.Nome.Should().Be(nome);
            usuario.Email.Should().Be(email);
            usuario.Ativo.Should().BeTrue();
            usuario.VerificarSenha(senha).Should().BeTrue();
        }

        [Fact]
        public void ValidarSenha_DeveRetornarTrue_ParaSenhaCorreta()
        {
            // Arrange
            var usuario = Usuario.Criar("Maria", "maria@teste.com", "senhaSegura");

            // Act
            var resultado = usuario.VerificarSenha("senhaSegura");

            // Assert
            resultado.Should().BeTrue();
        }

        [Fact]
        public void ValidarSenha_DeveRetornarFalse_ParaSenhaIncorreta()
        {
            // Arrange
            var usuario = Usuario.Criar("Carlos", "carlos@teste.com", "outraSenha");

            // Act
            var resultado = usuario.VerificarSenha("senhaErrada");

            // Assert
            resultado.Should().BeFalse();
        }

        [Fact]
        public void Desativar_DeveMudarStatusParaInativo()
        {
            // Arrange
            var usuario = Usuario.Criar("Ana", "ana@teste.com", "senhaTop");

            // Act
            usuario.Desativar();

            // Assert
            usuario.Ativo.Should().BeFalse();
        }
    }
}
