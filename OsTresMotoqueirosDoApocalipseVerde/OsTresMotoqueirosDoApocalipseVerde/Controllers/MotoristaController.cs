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
    [Tags("CRUD Motorista")]
    public class MotoristaController : ControllerBase
    {
        private readonly MotoristaUseCase _motoristaUseCase;
        private readonly CreateMotoristaRequestValidator _validationMotorista;

        public MotoristaController(MotoristaUseCase motoristaUseCase, CreateMotoristaRequestValidator validationMotorista)
        {
            _motoristaUseCase = motoristaUseCase;
            _validationMotorista = validationMotorista;
        }

        /// <summary>
        /// Get all Motoristas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateMotoristaResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMotorista()
        {
            var motoristas = await _motoristaUseCase.GetAllMotoristaAsync();
            return Ok(motoristas);
        }

        /// <summary>
        /// Get Motorista por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateMotoristaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMotorista(long id)
        {
            var motoristas = await _motoristaUseCase.GetByIdAsync(id);
            if (motoristas == null)
                return NotFound();

            return Ok(motoristas);
        }

        /// <summary>
        /// Cria um novo Motorista
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateMotoristaResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostMotorista([FromBody] CreateMotoristaRequest request)
        {
            _validationMotorista.ValidateAndThrow(request);

            var motoristaResponse = await _motoristaUseCase.CreateMotoristaAsync(request);
            return CreatedAtAction(nameof(GetMotorista), new { id = motoristaResponse.Id }, motoristaResponse);
        }

        /// <summary>
        /// Atualiza um Motorista existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutMotorista(long id, [FromBody] CreateMotoristaRequest request)
        {
            var updated = await _motoristaUseCase.UpdateMotoristaAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Motorista
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteMotorista(long id)
        {
            var deleted = await _motoristaUseCase.DeleteMotoristaAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
