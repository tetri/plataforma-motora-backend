using Microsoft.AspNetCore.Mvc;

using PlataformaMotora.Api.Models;
using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Domain.Repositories;

namespace PlataformaMotora.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoController(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        // GET api/veiculo/{placa}
        [HttpGet("{placa}")]
        public async Task<IActionResult> Get(string placa)
        {
            // Normaliza a placa para uppercase (a normalização também pode estar na entidade)
            var veiculo = await _veiculoRepository.ObterPorPlacaAsync(placa.ToUpperInvariant());
            if (veiculo == null)
                return NotFound();
            return Ok(veiculo);
        }

        // POST api/veiculo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VeiculoDto dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            try
            {
                // Cria a entidade usando o método de fábrica já presente na entidade Veiculo
                var veiculo = Veiculo.Criar(
                    dto.Placa,
                    dto.Marca,
                    dto.Modelo,
                    dto.AnoFabricacao,
                    dto.AnoModelo
                );

                // Verifica se já existe um veículo com essa placa
                var existente = await _veiculoRepository.ObterPorPlacaAsync(veiculo.Placa);
                if (existente != null)
                    return Conflict("Veículo com esta placa já existe.");

                await _veiculoRepository.AdicionarAsync(veiculo);
                return CreatedAtAction(nameof(Get), new { placa = veiculo.Placa }, veiculo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
