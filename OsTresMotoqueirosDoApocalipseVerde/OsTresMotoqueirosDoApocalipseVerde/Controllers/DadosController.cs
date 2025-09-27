using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;
using System.Net;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("CRUD Dados")]
    public class DadosController : ControllerBase
    {
        private readonly DadosUseCase _dadosUseCase;
        private readonly CreateDadosRequestValidator _validationDados;

        public DadosController(DadosUseCase dadosUseCase, CreateDadosRequestValidator validationDados)
        {
            _dadosUseCase = dadosUseCase;
            _validationDados = validationDados;
        }

        /// <summary>
        /// Get all Dados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateDadosResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDados()
        {
            var dados = await _dadosUseCase.GetAllDadosAsync();
            return Ok(dados);
        }

        /// <summary>
        /// Get Dados por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateDadosResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDados(long id)
        {
            var dados = await _dadosUseCase.GetByIdAsync(id);
            if (dados == null)
                return NotFound();

            return Ok(dados);
        }

        /// <summary>
        /// Cria um novo Dados
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateDadosResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostDados([FromBody] CreateDadosRequest request)
        {
            _validationDados.ValidateAndThrow(request);

            var dadosResponse = await _dadosUseCase.CreateDadosAsync(request);
            return CreatedAtAction(nameof(GetDados), new { id = dadosResponse.Id }, dadosResponse);
        }

        /// <summary>
        /// Atualiza um Dados existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutDados(long id, [FromBody] CreateDadosRequest request)
        {
            var updated = await _dadosUseCase.UpdateDadosAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Dados
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDados(long id)
        {
            var deleted = await _dadosUseCase.DeleteDadosAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
