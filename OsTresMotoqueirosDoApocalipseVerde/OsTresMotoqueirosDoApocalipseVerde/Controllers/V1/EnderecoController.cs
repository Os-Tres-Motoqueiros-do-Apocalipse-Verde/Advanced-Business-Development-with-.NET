using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;
using System.Net;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("CRUD Endereco")]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoUseCase _enderecoUseCase;
        private readonly CreateEnderecoRequestValidator _validationEndereco;

        public EnderecoController(EnderecoUseCase enderecoUseCase, CreateEnderecoRequestValidator validationEndereco)
        {
            _enderecoUseCase = enderecoUseCase;
            _validationEndereco = validationEndereco;
        }

        /// <summary>
        /// Retorna todos os Endereco com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 10)</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateEnderecoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEndereco([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            var endereco = await _enderecoUseCase.GetAllPagedAsync(page, pageSize);

            var result = endereco.Select(d => new
            {
                d.Id,
                d.Numero,
                d.Estado,
                d.CodigoPais,
                d.CodigoPostal,
                d.Complemento,
                d.Rua,
                links = new
                {
                    self = Url.Action(nameof(GetEnderecoById), new { id = d.Id }),
                    update = Url.Action(nameof(PutEndereco), new { id = d.Id }),
                    delete = Url.Action(nameof(DeleteEndereco), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = endereco.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Endereco pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateEnderecoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetEnderecoById(long id)
        {
            var endereco = await _enderecoUseCase.GetByIdAsync(id);
            if (endereco == null)
                return NotFound();

            var result = new
            {
                endereco.Id,
                endereco.Numero,
                endereco.Estado,
                endereco.CodigoPais,
                endereco.CodigoPostal,
                endereco.Complemento,
                endereco.Rua,
                links = new
                {
                    all = Url.Action(nameof(GetEndereco)),
                    update = Url.Action(nameof(PutEndereco), new { id = endereco.Id }),
                    delete = Url.Action(nameof(DeleteEndereco), new { id = endereco.Id })
                }
            };

            return Ok(result);
        }

        /// <summary>
        /// Cria um novo Endereco.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateEnderecoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostEndereco([FromBody] CreateEnderecoRequest request)
        {
            _validationEndereco.ValidateAndThrow(request);

            var enderecoResponse = await _enderecoUseCase.CreateEnderecoAsync(request);
            return CreatedAtAction(nameof(GetEnderecoById), new { id = enderecoResponse.Id }, enderecoResponse);
        }

        /// <summary>
        /// Atualiza um Endereco existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutEndereco(long id, [FromBody] CreateEnderecoRequest request)
        {
            var updated = await _enderecoUseCase.UpdateEnderecoAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Endereco existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteEndereco(long id)
        {
            var deleted = await _enderecoUseCase.DeleteEnderecoAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
