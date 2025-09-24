using System.Net;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("CRUD Motorista")]
    public class MotoristaController : ControllerBase
    {
        private readonly MotoristaUseCase _motoristaUseCase;
        private readonly CreateMotoristaDtoValidator _validationMotorista;

        public MotoristaController(
            MotoristaUseCase motoristaUseCase,
            CreateMotoristaDtoValidator validationMotorista)
        {
            _motoristaUseCase = motoristaUseCase;
            _validationMotorista = validationMotorista;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateMotoristaResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMotoristas()
        {
            var motoristas = await _motoristaUseCase.GetAllMotoristaAsync();
            return Ok(motoristas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateMotoristaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMotorista(long id)
        {
            var motorista = await _motoristaUseCase.GetByIdAsync(id);
            if (motorista == null)
                return NotFound();

            return Ok(motorista);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateMotoristaResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostMotorista([FromBody] CreateMotoristaRequest request)
        {
            var result = ((IValidator<CreateMotoristaRequest>)_validationMotorista).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var motoristaResponse = await _motoristaUseCase.CreateMotoristaAsync(request);
            return CreatedAtAction(nameof(GetMotorista), new { id = motoristaResponse.Id }, motoristaResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutMotorista(long id, [FromBody] CreateMotoristaRequest request)
        {
            var result = ((IValidator<CreateMotoristaRequest>)_validationMotorista).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updated = await _motoristaUseCase.UpdateMotoristaAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

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
