using PlataformaMotora.Domain.Entities;

namespace PlataformaMotora.Tests.Domain
{
    public class VeiculoTests
    {
        [Fact]
        public void CriarVeiculo_Valido_DeveCriarEntidade()
        {
            // Arrange
            var placa = "ABC1234";
            var marca = "Toyota";
            var modelo = "Corolla";
            var anoFabricacao = 2022;
            var anoModelo = 2022;
            var usuarioId = Guid.NewGuid();

            // Act
            var veiculo = Veiculo.Criar(placa, marca, modelo, anoFabricacao, anoModelo, usuarioId);

            // Assert
            Assert.NotNull(veiculo);
            Assert.Equal(placa, veiculo.Placa);
            Assert.Equal(marca, veiculo.Marca);
            Assert.Equal(modelo, veiculo.Modelo);
            Assert.Equal(anoFabricacao, veiculo.AnoFabricacao);
            Assert.Equal(anoModelo, veiculo.AnoModelo);
            Assert.Equal(usuarioId, veiculo.CriadoPor);
        }

        [Fact]
        public void CriarVeiculo_PlacaInvalida_DeveLancarExcecao()
        {
            // Arrange
            var placaInvalida = "PLACAINVALIDA";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                Veiculo.Criar(placaInvalida, "Toyota", "Corolla", 2022, 2022, Guid.NewGuid())
            );

            Assert.Equal("Placa inválida ou fora do padrão Mercosul/LatAm", exception.Message);
        }

        [Fact]
        public void CriarVeiculo_ComPlacaMercosulValida_DeveCriarEntidade()
        {
            // Arrange
            var placa = "ABC1D23";
            var marca = "Chevrolet";
            var modelo = "Onix";
            var anoFabricacao = 2021;
            var anoModelo = 2021;
            var usuarioId = Guid.NewGuid();

            // Act
            var veiculo = Veiculo.Criar(placa, marca, modelo, anoFabricacao, anoModelo, usuarioId);

            // Assert
            Assert.NotNull(veiculo);
            Assert.Equal(placa, veiculo.Placa);
        }

        [Fact]
        public void CriarVeiculo_AnoFabricacaoInvalido_DeveLancarExcecao()
        {
            // Arrange
            var placa = "ABC1234";
            var marca = "Honda";
            var modelo = "Civic";
            var anoFabricacao = 1800;  // ano inválido
            var anoModelo = 2022;
            var usuarioId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                Veiculo.Criar(placa, marca, modelo, anoFabricacao, anoModelo, usuarioId)
            );

            Assert.Equal("Ano de fabricação inválido", exception.Message);
        }

        [Fact]
        public void CriarVeiculo_AnoModeloMenorQueFabricacao_DeveLancarExcecao()
        {
            // Arrange
            var placa = "ABC1234";
            var marca = "Ford";
            var modelo = "Fiesta";
            var anoFabricacao = 2022;
            var anoModelo = 2021;  // ano modelo inválido
            var usuarioId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                Veiculo.Criar(placa, marca, modelo, anoFabricacao, anoModelo, usuarioId)
            );

            Assert.Equal("Ano do modelo não pode ser menor que o ano de fabricação", exception.Message);
        }
    }
}
