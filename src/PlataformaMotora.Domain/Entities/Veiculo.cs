using MercosulPlateValidator;

namespace PlataformaMotora.Domain.Entities;

public partial class Veiculo : Auditoria
{
    public Guid Id { get; private set; }
    public string? Placa { get; private set; }
    public string? Marca { get; private set; }
    public string? Modelo { get; private set; }
    public int AnoFabricacao { get; private set; }
    public int AnoModelo { get; private set; }

    protected Veiculo() { }

    private Veiculo(string placa, string marca, string modelo, int anoFabricacao, int anoModelo)
    {
        Id = Guid.NewGuid();
        Placa = NormalizePlaca(placa);
        Marca = marca;
        Modelo = modelo;
        AnoFabricacao = anoFabricacao;
        AnoModelo = anoModelo;

        Validar();
    }

    public static Veiculo Criar(string placa, string marca, string modelo, int anoFabricacao, int anoModelo, Guid usuarioId)
    {
        var veiculo = new Veiculo(placa, marca, modelo, anoFabricacao, anoModelo);
        veiculo.MarcarComoCriado(usuarioId);
        return veiculo;
    }

    private void Validar()
    {
        if (string.IsNullOrWhiteSpace(Placa))
            throw new ArgumentException("Placa é obrigatória");

        if (!MercosulPlate.ValidatePlate(Placa).IsValid)
            throw new ArgumentException("Placa inválida ou fora do padrão Mercosul/LatAm");

        if (string.IsNullOrWhiteSpace(Marca))
            throw new ArgumentException("Marca é obrigatória");

        if (string.IsNullOrWhiteSpace(Modelo))
            throw new ArgumentException("Modelo é obrigatório");

        if (AnoFabricacao < 1900 || AnoFabricacao > DateTime.Now.Year + 1)
            throw new ArgumentException("Ano de fabricação inválido");

        if (AnoModelo < AnoFabricacao)
            throw new ArgumentException("Ano do modelo não pode ser menor que o ano de fabricação");
    }

    private static string NormalizePlaca(string placa) => placa.Trim().ToUpperInvariant();
}
