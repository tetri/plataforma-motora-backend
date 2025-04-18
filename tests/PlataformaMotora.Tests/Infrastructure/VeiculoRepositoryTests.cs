using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Infrastructure.Persistence;
using PlataformaMotora.Infrastructure.Persistence.Repositories;

namespace PlataformaMotora.Tests.Infrastructure;

public class VeiculoRepositoryTests
{
    private static AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Deve_Adicionar_E_Buscar_Veiculo()
    {
        using var context = CriarContexto();
        var repo = new VeiculoRepository(context);

        var veiculo = Veiculo.Criar("ABC1D23", "Fiat", "Argo", 2021, 2021, Guid.NewGuid());

        await repo.AdicionarAsync(veiculo);

        var encontrado = await repo.ObterPorPlacaAsync("ABC1D23");

        encontrado.Should().NotBeNull();
        encontrado!.Modelo.Should().Be("Argo");
    }

    [Fact]
    public async Task Deve_Retornar_Null_Para_Placa_Desconhecida()
    {
        using var context = CriarContexto();
        var repo = new VeiculoRepository(context);

        var resultado = await repo.ObterPorPlacaAsync("XYZ9999");

        resultado.Should().BeNull();
    }
}
