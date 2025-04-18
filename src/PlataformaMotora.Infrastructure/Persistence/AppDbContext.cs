using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Domain.Entities;

namespace PlataformaMotora.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
