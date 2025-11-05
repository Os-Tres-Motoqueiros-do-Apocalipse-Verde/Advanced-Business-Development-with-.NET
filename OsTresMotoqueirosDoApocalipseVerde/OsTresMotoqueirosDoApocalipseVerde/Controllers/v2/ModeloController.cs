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
    [Tags("CRUD Modelo")]
    public class ModeloController : ControllerBase
    {
        private readonly ModeloUseCase _modeloUseCase;
        private readonly CreateModeloRequestValidator _validationModelo;

        public ModeloController(ModeloUseCase modeloUseCase, CreateModeloRequestValidator validationModelo)
        {
            _modeloUseCase = modeloUseCase;
            _validationModelo = validationModelo;
        }

        /// <summary>
        /// Retorna todos os Modelo com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 4)</param>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CreateModeloResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetModelo([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            var modelo = await _modeloUseCase.GetAllPagedAsync(page, pageSize);

            var result = modelo.Select(d => new
            {
                d.Id,
                d.NomeModelo,
                d.TipoCombustivel,
                links = new
                {
                    self = Url.Action(nameof(GetModeloById), new { id = d.Id }),
                    update = Url.Action(nameof(PutModelo), new { id = d.Id }),
                    delete = Url.Action(nameof(DeleteModelo), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = modelo.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Modelo pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateModeloResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetModeloById(long id)
        {
            var modelo = await _modeloUseCase.GetByIdAsync(id);
            if (modelo == null)
                return NotFound();


            return Ok(modelo);
        }

        /// <summary>
        /// Cria um novo Modelo.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateModeloResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostModelo([FromBody] CreateModeloRequest request)
        {
            _validationModelo.ValidateAndThrow(request);

            var modeloResponse = await _modeloUseCase.CreateModeloAsync(request);
            return CreatedAtAction(nameof(GetModeloById), new { id = modeloResponse.Id }, modeloResponse);
        }

        /// <summary>
        /// Atualiza um Modelo existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutModelo(long id, [FromBody] CreateModeloRequest request)
        {
            var updated = await _modeloUseCase.UpdateModeloAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Modelo existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteModelo(long id)
        {
            var deleted = await _modeloUseCase.DeleteModeloAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
