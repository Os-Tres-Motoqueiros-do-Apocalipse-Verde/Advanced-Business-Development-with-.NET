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
    [Tags("CRUD Motorista")]
    public class MotoristaController : ControllerBase
    {
        private readonly IRepository<Motorista> _repositoryMotorista;

        private readonly IRepository<Dados> _repositoryDados;

        private readonly MotoristaUseCase _MotoristaUseCase;

        private readonly CreateMotoristaRequestValidator _validationMotorista;

        public MotoristaController(IRepository<Motorista> repositoryMotorista, IRepository<Dados> repositoryDados, MotoristaUseCase motoristaUseCase, CreateMotoristaRequestValidator validationMotorista)
        {
            _repositoryMotorista = repositoryMotorista;
            _repositoryDados = repositoryDados;
            _MotoristaUseCase = motoristaUseCase;
            _validationMotorista = validationMotorista;
        }

        // GET: api/Motoristas
        /// <summary>
        /// Get todos os Motoristas
        /// </summary>
        /// <returns>Retorna a lista de motoristas</returns>
        /// <response code="200">Motoristas found</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IEnumerable<CreateMotoristaResponse>> GetMotoristas()
        {
            return await _MotoristaUseCase.GetAllMotoristaAsync();
        }

        // GET: api/Motoristas/2
        /// <summary>
        /// Get motorista por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateMotoristaResponse>> GetMotorista(int id)
        {
            var motorista = await _MotoristaUseCase.GetByIdAsync(id);

            if (motorista == null)
            {
                return NotFound();
            }

            return motorista;
        }

        // PUT: api/Motoristas/2
        /// <summary>
        /// PUT motorista por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutMotorista(int id, CreateMotoristaRequest updateRequest)
        {
            var sucesso = await _MotoristaUseCase.UpdateMotoristaAsync(id, updateRequest);

            if (!sucesso)
                return NotFound();

            return NoContent();
        }



        // POST: api/Motoristas
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Motorista>> PostMotorista(CreateMotoristaRequest createMotoristaRequest)
        {
            _validationMotorista.ValidateAndThrow(createMotoristaRequest);
          
            var motoristaResponse = await _MotoristaUseCase.CreateMotorista(createMotoristaRequest);
       
            return CreatedAtAction("GetMotorista", new { id = motoristaResponse.IdMotorista }, motoristaResponse);
        }

        // DELETE: api/Motorista/5
        /// <summary>
        /// Deleta um motorista pelo ID
        /// </summary>
        /// <param name="id">ID do motorista</param>
        /// <returns>NoContent se deletado com sucesso, NotFound se não existir</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteMotorista(int id)
        {
            var sucesso = await _MotoristaUseCase.DeleteMotoristaAsync(id);

            if (!sucesso)
                return NotFound();

            return NoContent();
        }

    }
}
