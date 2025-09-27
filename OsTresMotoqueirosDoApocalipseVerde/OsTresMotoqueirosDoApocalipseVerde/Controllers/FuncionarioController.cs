using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;
using System.Net;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [Route("api/[controller]")]
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
        /// Get all Funcionarios
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateFuncionarioResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFuncionario()
        {
            var funcionarios = await _funcionarioUseCase.GetAllFuncionarioAsync();
            return Ok(funcionarios);
        }

        /// <summary>
        /// Get Funcionario por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateFuncionarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFuncionario(long id)
        {
            var funcionarios = await _funcionarioUseCase.GetByIdAsync(id);
            if (funcionarios == null)
                return NotFound();

            return Ok(funcionarios);
        }

        /// <summary>
        /// Cria um novo Funcionario
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateFuncionarioResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostFuncionario([FromBody] CreateFuncionarioRequest request)
        {
            _validationFuncionario.ValidateAndThrow(request);

            var funcionarioResponse = await _funcionarioUseCase.CreateFuncionarioAsync(request);
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionarioResponse.Id }, funcionarioResponse);
        }

        /// <summary>
        /// Atualiza um Funcionario existente
        /// </summary>
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
        /// Deleta um Funcionario
        /// </summary>
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
