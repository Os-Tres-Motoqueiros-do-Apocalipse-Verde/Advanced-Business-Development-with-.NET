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
    [Tags("CRUD Setor")]
    public class SetorController : ControllerBase
    {
        private readonly SetorUseCase _setorUseCase;
        private readonly CreateSetorRequestValidator _validationSetor;

        public SetorController(SetorUseCase setorUseCase, CreateSetorRequestValidator validationSetor)
        {
            _setorUseCase = setorUseCase;
            _validationSetor = validationSetor;
        }

        /// <summary>
        /// Retorna todos os Setor com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 4)</param>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CreateSetorResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSetor([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            var setor = await _setorUseCase.GetAllPagedAsync(page, pageSize);

            var result = setor.Select(d => new
            {
                d.Id,
                d.NomeSetor,
                d.QtdMoto,
                links = new
                {
                    self = Url.Action(nameof(GetSetorById), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = setor.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Setor pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateSetorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSetorById(long id)
        {
            var setor = await _setorUseCase.GetByIdAsync(id);
            if (setor == null)
                return NotFound();


            return Ok(setor);
        }

        /// <summary>
        /// Cria um novo Setor.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateSetorResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostSetor([FromBody] CreateSetorRequest request)
        {
            _validationSetor.ValidateAndThrow(request);

            var setorResponse = await _setorUseCase.CreateSetorAsync(request);
            return CreatedAtAction(nameof(GetSetorById), new { id = setorResponse.Id }, setorResponse);
        }

        /// <summary>
        /// Atualiza um Setor existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutSetor(long id, [FromBody] CreateSetorRequest request)
        {
            var updated = await _setorUseCase.UpdateSetorAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Setor existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
