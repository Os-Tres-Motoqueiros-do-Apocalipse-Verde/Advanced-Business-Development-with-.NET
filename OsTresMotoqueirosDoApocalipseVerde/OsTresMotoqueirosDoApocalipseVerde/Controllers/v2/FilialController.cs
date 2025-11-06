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
    [Tags("CRUD Filial")]
    public class FilialController : ControllerBase
    {
        private readonly FilialUseCase _filialUseCase;
        private readonly CreateFilialRequestValidator _validationFilial;

        public FilialController(FilialUseCase filialUseCase, CreateFilialRequestValidator validationFilial)
        {
            _filialUseCase = filialUseCase;
            _validationFilial = validationFilial;
        }

        /// <summary>
        /// Retorna todos os Filial com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 10)</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateFilialResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFilial([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var filial = await _filialUseCase.GetAllPagedAsync(page, pageSize);

            var result = filial.Select(d => new
            {
                d.Id,
                d.NomeFilial,
                links = new
                {
                    self = Url.Action(nameof(GetFilialById), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = filial.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Filial pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateFilialResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFilialById(long id)
        {
            var filial = await _filialUseCase.GetByIdAsync(id);
            if (filial == null)
                return NotFound();


            return Ok(filial);
        }

        /// <summary>
        /// Cria um novo Filial.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateFilialResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostFilial([FromBody] CreateFilialRequest request)
        {
            _validationFilial.ValidateAndThrow(request);

            var filialResponse = await _filialUseCase.CreateFilialAsync(request);
            return CreatedAtAction(nameof(GetFilialById), new { id = filialResponse.Id }, filialResponse);
        }

        /// <summary>
        /// Atualiza um Filial existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutFilial(long id, [FromBody] CreateFilialRequest request)
        {
            var updated = await _filialUseCase.UpdateFilialAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Filial existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
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
