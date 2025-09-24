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
    [Tags("CRUD Filial")]
    public class FilialController : ControllerBase
    {
        private readonly FilialUseCase _filialUseCase;
        private readonly CreateFilialDtoValidator _validationFilial;

        public FilialController(
            FilialUseCase filialUseCase,
            CreateFilialDtoValidator validationFilial)
        {
            _filialUseCase = filialUseCase;
            _validationFilial = validationFilial;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateFilialResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFilials()
        {
            var filiais = await _filialUseCase.GetAllFiliaisAsync();
            return Ok(filiais);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateFilialResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFilial(long id)
        {
            var filial = await _filialUseCase.GetByIdAsync(id);
            if (filial == null)
                return NotFound();

            return Ok(filial);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateFilialResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostFilial([FromBody] CreateFilialRequest request)
        {
            var result = ((IValidator<CreateFilialRequest>)_validationFilial).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var filialResponse = await _filialUseCase.CreateFilialAsync(request);
            return CreatedAtAction(nameof(GetFilial), new { id = filialResponse.Id }, filialResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutFilial(long id, [FromBody] CreateFilialRequest request)
        {
            var result = ((IValidator<CreateFilialRequest>)_validationFilial).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updated = await _filialUseCase.UpdateFilialAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteFilial(long id)
        {
            var deleted = await _filialUseCase.DeleteFilialAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
