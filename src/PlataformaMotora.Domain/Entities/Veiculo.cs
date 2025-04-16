namespace PlataformaMotora.Domain.Entities;

public partial class Veiculo
{
    public Guid Id { get; private set; }
    public string Placa { get; private set; }
    public string Marca { get; private set; }
    public string Modelo { get; private set; }
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

    public static Veiculo Criar(string placa, string marca, string modelo, int anoFabricacao, int anoModelo)
    {
        return new Veiculo(placa, marca, modelo, anoFabricacao, anoModelo);
    }

    private void Validar()
    {
        if (string.IsNullOrWhiteSpace(Placa))
            throw new ArgumentException("Placa é obrigatória");

        if (!PlacaMercosul().IsMatch(Placa))
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

    private string NormalizePlaca(string placa) => placa.Trim().ToUpperInvariant();

    [System.Text.RegularExpressions.GeneratedRegex(@"^[A-Z]{3}[0-9]{4}$|^[A-Z]{3}[0-9][A-Z][0-9]{2}$|^[A-Z]{2}[0-9]{3}[A-Z]{2}$|^[A-Z]{2}[0-9]{4}$|^[A-Z]{1}[A-Z]{2}[0-9]{4}$")]
    private static partial System.Text.RegularExpressions.Regex PlacaMercosul();
}
