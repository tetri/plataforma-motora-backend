using Microsoft.AspNetCore.Mvc;

using PlataformaMotora.API.Extensions;
using PlataformaMotora.Application.DTOs;
using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Domain.Repositories;

namespace PlataformaMotora.Api.Controllers
{
    /// <summary>
    /// Controller para gerenciar veículos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController(IVeiculoRepository veiculoRepository) : ControllerBase
    {
        private readonly IVeiculoRepository _veiculoRepository = veiculoRepository;

        /// <summary>
        /// Obtém um veículo pelo número da placa.
        /// </summary>
        /// <param name="placa"></param>
        /// <returns></returns>
        [HttpGet("{placa}")]
        public async Task<IActionResult> Get(string placa)
        {
            var veiculo = await _veiculoRepository.ObterPorPlacaAsync(placa.ToUpperInvariant());
            if (veiculo == null)
                return NotFound();
            return Ok(veiculo);
        }

        /// <summary>
        /// Cria um novo veículo.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VeiculoDto dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            var usuarioId = HttpContext.ObterUsuarioId();
            if (usuarioId == null)
                return Unauthorized();

            try
            {
                var veiculo = Veiculo.Criar(
                    dto.Placa!,
                    dto.Marca!,
                    dto.Modelo!,
                    dto.AnoFabricacao,
                    dto.AnoModelo,
                    usuarioId.Value
                );

                var existente = await _veiculoRepository.ObterPorPlacaAsync(veiculo.Placa!);
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
