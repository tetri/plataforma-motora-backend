using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Domain.Repositories;

namespace PlataformaMotora.Infrastructure.Persistence.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AppDbContext _context;
        public VeiculoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Veiculo?> ObterPorPlacaAsync(string placa)
        {
            // Considera que a placa já está normalizada para uppercase
            return await _context.Veiculos.FirstOrDefaultAsync(v => v.Placa == placa);
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
        }
    }
}
