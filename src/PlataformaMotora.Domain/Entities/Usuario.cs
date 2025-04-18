using PlataformaMotora.Domain.Constants;

namespace PlataformaMotora.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }

    public static Usuario Criar(string nome, string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email inválido.");

        if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
            throw new ArgumentException("Senha deve ter no mínimo 6 caracteres.");

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

        return new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = nome,
            Email = email,
            SenhaHash = senhaHash,
            Ativo = true,
            CriadoEm = DateTime.UtcNow
        };
    }

    public void Desativar()
    {
        Ativo = false;
    }

    public bool VerificarSenha(string senha)
    {
        return BCrypt.Net.BCrypt.Verify(senha, SenhaHash);
    }
}
