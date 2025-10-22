using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Services;
using System.Security.Claims;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Endpoint de login (gera o token JWT)
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Aqui seria a validação do usuário no banco de dados
            // Por enquanto, é apenas uma simulação simples:
            if (request.Username == "admin" && request.Password == "123")
            {
                // Gera o token com nome de usuário e papel (role)
                var token = _tokenService.GenerateToken(request.Username, "Admin");
                return Ok(new { token });
            }

            return Unauthorized("Usuário ou senha inválidos");
        }

        /// <summary>
        /// Endpoint protegido por JWT
        /// </summary>
        [HttpGet("dados-protegidos")]
        [Authorize]
        public IActionResult GetDadosProtegidos()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok($"Bem-vindo, {username}! Seu papel é: {role}. Você acessou um endpoint protegido 🔐");
        }
    }

    /// <summary>
    /// Modelo usado para login
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
