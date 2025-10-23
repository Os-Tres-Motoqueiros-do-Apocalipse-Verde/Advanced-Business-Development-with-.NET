using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using OsTresMotoqueirosDoApocalipseVerde.Services;
using System.Security.Claims;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly AppDbContext _context;

        public AuthController(TokenService tokenService, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        /// <summary>
        /// Endpoint de login (gera o token JWT)
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Busca o usuário no banco Oracle
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos");

            // Gera token com papel (Role) vindo do banco
            var token = _tokenService.GenerateToken(usuario.Username, usuario.Role.ToString());
            return Ok(new { token });
        }

        /// <summary>
        /// Endpoint protegido com JWT
        /// </summary>
        [HttpGet("dados-protegidos")]
        [Authorize]
        public IActionResult GetDadosProtegidos()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok($"Bem-vindo, {username}! Seu papel é: {role}. Você acessou um endpoint protegido 🔐");
        }

        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}
