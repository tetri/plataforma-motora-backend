using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Domain.Repositories;

namespace PlataformaMotora.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(v => v.Email == email);
        }

        public async Task AdicionarAsync(Usuario veiculo)
        {
            _context.Usuarios.Add(veiculo);
            await _context.SaveChangesAsync();
        }
    }
}
