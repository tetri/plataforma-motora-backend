namespace PlataformaMotora.Application.DTOs
{
    public record VeiculoDto
    {
        public string? Placa { get; init; }
        public string? Marca { get; init; }
        public string? Modelo { get; init; }
        public int AnoFabricacao { get; init; }
        public int AnoModelo { get; init; }
    }
}