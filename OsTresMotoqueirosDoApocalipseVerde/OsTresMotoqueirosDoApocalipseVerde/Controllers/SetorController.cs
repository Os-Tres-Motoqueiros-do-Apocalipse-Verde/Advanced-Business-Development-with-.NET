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
    [Tags("CRUD Setor")]
    public class SetorController : ControllerBase
    {
        private readonly SetorUseCase _setorUseCase;
        private readonly CreateSetorDtoValidator _validationSetor;

        public SetorController(
            SetorUseCase setorUseCase,
            CreateSetorDtoValidator validationSetor)
        {
            _setorUseCase = setorUseCase;
            _validationSetor = validationSetor;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateSetorResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSetor()
        {
            var setores = await _setorUseCase.GetAllSetorAsync();
            return Ok(setores);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateSetorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSetor(long id)
        {
            var setor = await _setorUseCase.GetByIdAsync(id);
            if (setor == null)
                return NotFound();

            return Ok(setor);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateSetorResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostSetor([FromBody] CreateSetorRequest request)
        {
            var result = ((IValidator<CreateSetorRequest>)_validationSetor).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var setorResponse = await _setorUseCase.CreateSetorAsync(request);
            return CreatedAtAction(nameof(GetSetor), new { id = setorResponse.Id }, setorResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutSetor(long id, [FromBody] CreateSetorRequest request)
        {
            var result = ((IValidator<CreateSetorRequest>)_validationSetor).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updated = await _setorUseCase.UpdateSetorAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSetor(long id)
        {
            var deleted = await _setorUseCase.DeleteSetorAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
