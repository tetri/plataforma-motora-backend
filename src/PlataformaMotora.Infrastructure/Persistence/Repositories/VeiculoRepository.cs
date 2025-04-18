using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Domain.Repositories;

namespace PlataformaMotora.Infrastructure.Persistence.Repositories
{
    public class VeiculoRepository(AppDbContext context) : IVeiculoRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Veiculo?> ObterPorPlacaAsync(string placa)
        {
            return await _context.Veiculos.FirstOrDefaultAsync(v => v.Placa == placa);
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
        }
    }
}
