using Microsoft.Extensions.Configuration;

using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Infrastructure.Services;

namespace PlataformaMotora.Tests.Infrastructure.Services
{
    public class JwtServiceTests
    {
        private readonly JwtService _jwtService;

        public JwtServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "JWT_SECRET", "chave-muito-secreta-teste-123456" },
                { "JWT_ISSUER", "PlataformaMotora" },
                { "JWT_AUDIENCE", "PlataformaMotora" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            _jwtService = new JwtService(configuration);
        }

        [Fact]
        public void GerarToken_DeveRetornarTokenValido()
        {
            var usuario = Usuario.Criar("João", "joao@motora.com", "senhaTop");
            var token = _jwtService.GerarToken(usuario);

            Assert.False(string.IsNullOrWhiteSpace(token));
        }
    }
}