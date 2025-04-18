using Microsoft.AspNetCore.Mvc;

using PlataformaMotora.Api.Models;
using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Domain.Repositories;

namespace PlataformaMotora.Api.Controllers
{
    /// <summary>
    /// Controller para gerenciar usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioRepository UsuarioRepository) : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository = UsuarioRepository;

        /// <summary>
        /// Obtém um usuário pelo e-mail.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(email);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioCadastroDto dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            try
            {
                var usuario = Usuario.Criar(
                    dto.Nome,
                    dto.Email,
                    dto.Senha
                );

                var existente = await _usuarioRepository.ObterPorEmailAsync(usuario.Email);
                if (existente != null)
                    return Conflict("Usuário com este e-mail já existe.");

                await _usuarioRepository.AdicionarAsync(usuario);
                return CreatedAtAction(nameof(Get), new { email = usuario.Email }, usuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}