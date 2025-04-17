using PlataformaMotora.Domain.Entities;

namespace PlataformaMotora.Domain.Repositories
{
    public interface IVeiculoRepository
    {
        Task<Veiculo?> ObterPorPlacaAsync(string placa);
        Task AdicionarAsync(Veiculo veiculo);
    }
}
