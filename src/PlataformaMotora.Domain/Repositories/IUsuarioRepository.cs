using PlataformaMotora.Domain.Entities;

namespace PlataformaMotora.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task AdicionarAsync(Usuario usuario);
    }
}
