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
    [Tags("CRUD Patio")]
    public class PatioController : ControllerBase
    {
        private readonly PatioUseCase _patioUseCase;
        private readonly CreatePatioRequestValidator _validationPatio;

        public PatioController(PatioUseCase patioUseCase, CreatePatioRequestValidator validationPatio)
        {
            _patioUseCase = patioUseCase;
            _validationPatio = validationPatio;
        }

        /// <summary>
        /// Retorna todos os Patio com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 4)</param>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CreatePatioResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPatio([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            var patio = await _patioUseCase.GetAllPagedAsync(page, pageSize);

            var result = patio.Select(d => new
            {
                d.Id,
                d.Localizacao,
                d.TotalMotos,
                links = new
                {
                    self = Url.Action(nameof(GetPatioById), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = patio.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Patio pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreatePatioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPatioById(long id)
        {
            var patio = await _patioUseCase.GetByIdAsync(id);
            if (patio == null)
                return NotFound();


            return Ok(patio);
        }

        /// <summary>
        /// Cria um novo Patio.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreatePatioResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostPatio([FromBody] CreatePatioRequest request)
        {
            _validationPatio.ValidateAndThrow(request);

            var patioResponse = await _patioUseCase.CreatePatioAsync(request);
            return CreatedAtAction(nameof(GetPatioById), new { id = patioResponse.Id }, patioResponse);
        }

        /// <summary>
        /// Atualiza um Patio existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutPatio(long id, [FromBody] CreatePatioRequest request)
        {
            var updated = await _patioUseCase.UpdatePatioAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Patio existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
