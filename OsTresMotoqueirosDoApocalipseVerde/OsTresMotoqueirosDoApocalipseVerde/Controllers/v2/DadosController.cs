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
        /// Retorna todos os Dados com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 4)</param>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CreateDadosResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDados([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            var dados = await _dadosUseCase.GetAllPagedAsync(page, pageSize);

            var result = dados.Select(d => new
            {
                d.Id,
                d.Nome,
                d.Email,
                links = new
                {
                    self = Url.Action(nameof(GetDadosById), new { id = d.Id }),
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = dados.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Dados pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateDadosResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDadosById(long id)
        {
            var dados = await _dadosUseCase.GetByIdAsync(id);
            if (dados == null)
                return NotFound();

            return Ok(dados);
        }

        /// <summary>
        /// Cria um novo Dados.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateDadosResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostDados([FromBody] CreateDadosRequest request)
        {
            _validationDados.ValidateAndThrow(request);

            var dadosResponse = await _dadosUseCase.CreateDadosAsync(request);
            return CreatedAtAction(nameof(GetDadosById), new { id = dadosResponse.Id }, dadosResponse);
        }

        /// <summary>
        /// Atualiza um Dados existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        /// Deleta um Dados existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
