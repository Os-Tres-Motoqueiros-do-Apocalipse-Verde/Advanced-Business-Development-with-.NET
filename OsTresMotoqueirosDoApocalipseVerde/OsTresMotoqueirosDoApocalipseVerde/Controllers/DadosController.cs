using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCases;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validador;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Persistence;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("CRUD Dados")]
    public class DadosController : ControllerBase
    {

        private readonly IRepository<Dados> _repositoryDados;

        private readonly DadosUseCase _DadosUseCase;

        private readonly CreateDadosRequestValidator _validationDados;

        public DadosController(IRepository<Dados> repositoryDados,  DadosUseCase dadosUseCase, CreateDadosRequestValidator validationDados)
        {

            _repositoryDados = repositoryDados;
            _DadosUseCase = dadosUseCase;
            _validationDados = validationDados;
        }

        // GET: api/Dados
        /// <summary>
        /// Get todos os Dados
        /// </summary>
        /// <returns>Retorna a lista de dados</returns>
        /// <response code="200">Dados found</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IEnumerable<CreateDadosResponse>> GetDados()
        {
            return await _DadosUseCase.GetAllDadosAsync();
        }

        // GET: api/Dados/2
        /// <summary>
        /// Get dados por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateDadosResponse>> GetDados(int id)
        {
            var dados = await _DadosUseCase.GetByIdAsync(id);

            if (dados == null)
            {
                return NotFound();
            }

            return dados;
        }

        // POST: api/dados
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Dados>> PostDados(CreateDadosRequest createDadosRequest)
        {
            _validationDados.ValidateAndThrow(createDadosRequest);

            var dadosResponse = await _DadosUseCase.CreateDados(createDadosRequest);

            return CreatedAtAction("GetDados", new { id = dadosResponse.Id }, dadosResponse);
        }

        // PUT: api/Dados/2
        /// <summary>
        /// PUT Dados por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutDados(int id, CreateDadosRequest updateRequest)
        {
            var sucesso = await _DadosUseCase.UpdateDadosAsync(id, updateRequest);

            if (!sucesso)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Dados/5
        /// <summary>
        /// Deleta um dados pelo ID
        /// </summary>
        /// <param name="id">ID do motorista</param>
        /// <returns>NoContent se deletado com sucesso, NotFound se não existir</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteDados(int id)
        {
            var sucesso = await _DadosUseCase.DeleteDadosAsync(id);

            if (!sucesso)
                return NotFound();

            return NoContent();
        }
    }
}
