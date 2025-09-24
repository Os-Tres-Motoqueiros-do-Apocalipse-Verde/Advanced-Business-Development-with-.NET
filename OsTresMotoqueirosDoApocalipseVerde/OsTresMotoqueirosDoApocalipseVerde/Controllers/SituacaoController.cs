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
    [Tags("CRUD Situacao")]
    public class SituacaoController : ControllerBase
    {
        private readonly SituacaoUseCase _situacaoUseCase;
        private readonly CreateSituacaoDtoValidator _validationSituacao;

        public SituacaoController(
            SituacaoUseCase situacaoUseCase,
            CreateSituacaoDtoValidator validationSituacao)
        {
            _situacaoUseCase = situacaoUseCase;
            _validationSituacao = validationSituacao;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateSituacaoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSituacao()
        {
            var situacoes = await _situacaoUseCase.GetAllSituacaoAsync();
            return Ok(situacoes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateSituacaoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSituacao(long id)
        {
            var situacao = await _situacaoUseCase.GetByIdAsync(id);
            if (situacao == null)
                return NotFound();

            return Ok(situacao);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateSituacaoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostSituacao([FromBody] CreateSituacaoRequest request)
        {
            var result = ((IValidator<CreateSituacaoRequest>)_validationSituacao).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var situacaoResponse = await _situacaoUseCase.CreateSituacaoAsync(request);
            return CreatedAtAction(nameof(GetSituacao), new { id = situacaoResponse.Id }, situacaoResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutSituacao(long id, [FromBody] CreateSituacaoRequest request)
        {
            var result = ((IValidator<CreateSituacaoRequest>)_validationSituacao).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updated = await _situacaoUseCase.UpdateSituacaoAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSituacao(long id)
        {
            var deleted = await _situacaoUseCase.DeleteSituacaoAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
