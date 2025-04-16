using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Domain.Entities;

namespace PlataformaMotora.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
