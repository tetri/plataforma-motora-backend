using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Infrastructure.Persistence;

namespace PlataformaMotora.Infrastructure.Services;

public class SeedService(AppDbContext context, IConfiguration configuration)
{
    private readonly AppDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    public async Task SeedAsync()
    {
        if (!await _context.Usuarios.AnyAsync())
        {
            string? email = _configuration["SYSTEMUSER_EMAIL"];
            string? senha = _configuration["SYSTEMUSER_PASSWORD"];

            var usuario = Usuario.Criar("root", email!, senha!);

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();
        }
    }
}
