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
    [Tags("CRUD Moto")]
    public class MotoController : ControllerBase
    {
        private readonly MotoUseCase _motoUseCase;
        private readonly CreateMotoRequestValidator _validationMoto;

        public MotoController(MotoUseCase motoUseCase, CreateMotoRequestValidator validationMoto)
        {
            _motoUseCase = motoUseCase;
            _validationMoto = validationMoto;
        }

        /// <summary>
        /// Retorna todos os Moto com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 10)</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateMotoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMoto([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var moto = await _motoUseCase.GetAllPagedAsync(page, pageSize);

            var result = moto.Select(d => new
            {
                d.Id,
                d.Placa,
                d.Chassi,
                links = new
                {
                    self = Url.Action(nameof(GetMotoById), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = moto.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Moto pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateMotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMotoById(long id)
        {
            var moto = await _motoUseCase.GetByIdAsync(id);
            if (moto == null)
                return NotFound();

            return Ok(moto);
        }

        /// <summary>
        /// Cria um novo Moto.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateMotoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostMoto([FromBody] CreateMotoRequest request)
        {
            _validationMoto.ValidateAndThrow(request);

            var motoResponse = await _motoUseCase.CreateMotoAsync(request);
            return CreatedAtAction(nameof(GetMotoById), new { id = motoResponse.Id }, motoResponse);
        }

        /// <summary>
        /// Atualiza um Moto existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutMoto(long id, [FromBody] CreateMotoRequest request)
        {
            var updated = await _motoUseCase.UpdateMotoAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Moto existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteMoto(long id)
        {
            var deleted = await _motoUseCase.DeleteMotoAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
