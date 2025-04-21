using PlataformaMotora.Domain.Entities;

namespace PlataformaMotora.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GerarToken(Usuario usuario);
    }
}
