using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlataformaMotora.Application.DTOs.Requests;
using PlataformaMotora.Application.Interfaces.Services;
using PlataformaMotora.Domain.Entities;
using PlataformaMotora.Domain.Repositories;

namespace PlataformaMotora.Api.Controllers
{
    /// <summary>
    /// Controller para autenticação de usuários.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("auth")]
    public class AuthController(IUsuarioRepository usuarioRepository, IJwtService jwtService) : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IJwtService _jwtService = jwtService;

        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);
            if (usuario == null || !usuario.VerificarSenha(request.Senha))
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _jwtService.GerarToken(usuario);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var existente = await _usuarioRepository.ObterPorEmailAsync(request.Email);
            if (existente != null)
                return Conflict("E-mail já cadastrado.");

            var usuario = Usuario.Criar(request.Nome, request.Email, request.Senha);
            await _usuarioRepository.AdicionarAsync(usuario);
            return Ok("Usuário criado com sucesso.");
        }
    }
}