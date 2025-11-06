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
    [Tags("CRUD Funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioUseCase _funcionarioUseCase;
        private readonly CreateFuncionarioRequestValidator _validationFuncionario;

        public FuncionarioController(FuncionarioUseCase funcionarioUseCase, CreateFuncionarioRequestValidator validationFuncionario)
        {
            _funcionarioUseCase = funcionarioUseCase;
            _validationFuncionario = validationFuncionario;
        }

        /// <summary>
        /// Retorna todos os Funcionario com paginação.
        /// </summary>
        /// <param name="page">Número da página (default = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (default = 10)</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateFuncionarioResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFuncionario([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var funcionario = await _funcionarioUseCase.GetAllPagedAsync(page, pageSize);

            var result = funcionario.Select(d => new
            {
                d.Id,
                d.Cargo,
                d.FilialId,
                links = new
                {
                    self = Url.Action(nameof(GetFuncionarioById), new { id = d.Id })
                }
            });

            return Ok(new
            {
                page,
                pageSize,
                totalItems = funcionario.Count(),
                items = result
            });
        }

        /// <summary>
        /// Retorna um Funcionario pelo ID.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateFuncionarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFuncionarioById(long id)
        {
            var funcionario = await _funcionarioUseCase.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound();


            return Ok(funcionario);
        }

        /// <summary>
        /// Cria um novo Funcionario.
        /// </summary>
        /// <param name="request">Payload para criação</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateFuncionarioResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostFuncionario([FromBody] CreateFuncionarioRequest request)
        {
            _validationFuncionario.ValidateAndThrow(request);

            var funcionarioResponse = await _funcionarioUseCase.CreateFuncionarioAsync(request);
            return CreatedAtAction(nameof(GetFuncionarioById), new { id = funcionarioResponse.Id }, funcionarioResponse);
        }

        /// <summary>
        /// Atualiza um Funcionario existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <param name="request">Payload para atualização</param>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutFuncionario(long id, [FromBody] CreateFuncionarioRequest request)
        {
            var updated = await _funcionarioUseCase.UpdateFuncionarioAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Funcionario existente.
        /// </summary>
        /// <param name="id">ID do registro</param>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteFuncionario(long id)
        {
            var deleted = await _funcionarioUseCase.DeleteFuncionarioAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
