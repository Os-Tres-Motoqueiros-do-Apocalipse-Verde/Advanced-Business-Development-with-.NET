using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;
using System.Net;
using Microsoft.AspNetCore.Authorization;


namespace OsTresMotoqueirosDoApocalipseVerde.Controllers.V2
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    [Tags("CRUD Situacao")]
    public class SituacaoController : ControllerBase
    {
        private readonly SituacaoUseCase _situacaoUseCase;
        private readonly CreateSituacaoRequestValidator _validationSituacao;

        public SituacaoController(SituacaoUseCase situacaoUseCase, CreateSituacaoRequestValidator validationSituacao)
        {
            _situacaoUseCase = situacaoUseCase;
            _validationSituacao = validationSituacao;
        }

        /// <summary>
        /// Retorna todos os Situacao com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 4)</param>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CreateSituacaoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSituacao([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            var situacao = await _situacaoUseCase.GetAllPagedAsync(page, pageSize);

            var result = situacao.Select(d => new
            {
                d.Id,
                d.Nome,
                d.Status,
                links = new
                {
                    self = Url.Action(nameof(GetSituacaoById), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = situacao.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Situacao pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateSituacaoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSituacaoById(long id)
        {
            var situacao = await _situacaoUseCase.GetByIdAsync(id);
            if (situacao == null)
                return NotFound();


            return Ok(situacao);
        }

        /// <summary>
        /// Cria um novo Situacao.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateSituacaoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostSituacao([FromBody] CreateSituacaoRequest request)
        {
            _validationSituacao.ValidateAndThrow(request);

            var situacaoResponse = await _situacaoUseCase.CreateSituacaoAsync(request);
            return CreatedAtAction(nameof(GetSituacaoById), new { id = situacaoResponse.Id }, situacaoResponse);
        }

        /// <summary>
        /// Atualiza um Situacao existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutSituacao(long id, [FromBody] CreateSituacaoRequest request)
        {
            var updated = await _situacaoUseCase.UpdateSituacaoAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Situacao existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
