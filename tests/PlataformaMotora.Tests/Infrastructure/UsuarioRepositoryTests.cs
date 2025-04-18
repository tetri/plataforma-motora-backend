using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Infrastructure.Persistence;
using PlataformaMotora.Infrastructure.Persistence.Repositories;

namespace PlataformaMotora.Tests.Infrastructure;

public class UsuarioRepositoryTests
{
    private static AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Deve_Adicionar_E_Buscar_Usuario()
    {
        using var context = CriarContexto();
        var repo = new UsuarioRepository(context);

        var usuario = Usuario.Criar("jair", "jair@motora.com", "senhaSegura");

        await repo.AdicionarAsync(usuario);

        var encontrado = await repo.ObterPorEmailAsync("jair@motora.com");

        encontrado.Should().NotBeNull();
        encontrado!.Email.Should().Be("jair@motora.com");
    }

    [Fact]
    public async Task Deve_Retornar_Null_Para_Email_Inexistente()
    {
        using var context = CriarContexto();
        var repo = new UsuarioRepository(context);

        var resultado = await repo.ObterPorEmailAsync("joao@motora.com");

        resultado.Should().BeNull();
    }
}
