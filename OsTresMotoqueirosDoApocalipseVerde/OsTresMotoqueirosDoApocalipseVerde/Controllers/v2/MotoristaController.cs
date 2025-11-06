using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;
using System.Net;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
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
        /// Retorna todos os Motorista com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 10)</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateMotoristaResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMotorista([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var motorista = await _motoristaUseCase.GetAllPagedAsync(page, pageSize);

            var result = motorista.Select(d => new
            {
                d.Id,
                d.Plano,
                links = new
                {
                    self = Url.Action(nameof(GetMotoristaById), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = motorista.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Motorista pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateMotoristaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMotoristaById(long id)
        {
            var motorista = await _motoristaUseCase.GetByIdAsync(id);
            if (motorista == null)
                return NotFound();

            return Ok(motorista);
        }

        /// <summary>
        /// Cria um novo Motorista.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateMotoristaResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostMotorista([FromBody] CreateMotoristaRequest request)
        {
            _validationMotorista.ValidateAndThrow(request);

            var motoristaResponse = await _motoristaUseCase.CreateMotoristaAsync(request);
            return CreatedAtAction(nameof(GetMotoristaById), new { id = motoristaResponse.Id }, motoristaResponse);
        }

        /// <summary>
        /// Atualiza um Motorista existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
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
        /// Deleta um Motorista existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
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
