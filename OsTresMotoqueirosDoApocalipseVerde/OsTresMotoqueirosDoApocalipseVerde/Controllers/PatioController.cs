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
    [Tags("CRUD Patio")]
    public class PatioController : ControllerBase
    {
        private readonly PatioUseCase _patioUseCase;
        private readonly CreatePatioDtoValidator _validationPatio;

        public PatioController(
            PatioUseCase patioUseCase,
            CreatePatioDtoValidator validationPatio)
        {
            _patioUseCase = patioUseCase;
            _validationPatio = validationPatio;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreatePatioResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPatios()
        {
            var patios = await _patioUseCase.GetAllPatioAsync();
            return Ok(patios);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreatePatioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPatio(long id)
        {
            var patio = await _patioUseCase.GetByIdAsync(id);
            if (patio == null)
                return NotFound();

            return Ok(patio);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatePatioResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostPatio([FromBody] CreatePatioRequest request)
        {
            var result = ((IValidator<CreatePatioRequest>)_validationPatio).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var patioResponse = await _patioUseCase.CreatePatioAsync(request);
            return CreatedAtAction(nameof(GetPatio), new { id = patioResponse.Id }, patioResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutPatio(long id, [FromBody] CreatePatioRequest request)
        {
            var result = ((IValidator<CreatePatioRequest>)_validationPatio).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updated = await _patioUseCase.UpdatePatioAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePatio(long id)
        {
            var deleted = await _patioUseCase.DeletePatioAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
