using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Infrastructure.Persistence;

namespace PlataformaMotora.Infrastructure.Services;

public class SeedService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public SeedService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task SeedAsync()
    {
        if (!await _context.Usuarios.AnyAsync())
        {
            var email = _configuration["SYSTEMUSER_EMAIL"];
            var senha = _configuration["SYSTEMUSER_PASSWORD"];

            var usuario = Usuario.Criar("root", email, senha);

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();
        }
    }
}
