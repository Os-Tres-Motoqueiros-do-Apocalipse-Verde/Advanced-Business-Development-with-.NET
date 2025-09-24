using System.Net;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("CRUD Funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioUseCase _funcionarioUseCase;
        private readonly CreateFuncionarioDtoValidator _validationFuncionario;

        public FuncionarioController(
            FuncionarioUseCase funcionarioUseCase,
            CreateFuncionarioDtoValidator validationFuncionario)
        {
            _funcionarioUseCase = funcionarioUseCase;
            _validationFuncionario = validationFuncionario;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateFuncionarioResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFuncionarios()
        {
            var funcionarios = await _funcionarioUseCase.GetAllFuncionarioAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateFuncionarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFuncionario(long id)
        {
            var funcionario = await _funcionarioUseCase.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound();

            return Ok(funcionario);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateFuncionarioResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostFuncionario([FromBody] CreateFuncionarioRequest request)
        {
            var result = ((IValidator<CreateFuncionarioRequest>)_validationFuncionario).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var funcionarioResponse = await _funcionarioUseCase.CreateFuncionarioAsync(request);
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionarioResponse.Id }, funcionarioResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutFuncionario(long id, [FromBody] CreateFuncionarioRequest request)
        {
            var result = ((IValidator<CreateFuncionarioRequest>)_validationFuncionario).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updated = await _funcionarioUseCase.UpdateFuncionarioAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

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
