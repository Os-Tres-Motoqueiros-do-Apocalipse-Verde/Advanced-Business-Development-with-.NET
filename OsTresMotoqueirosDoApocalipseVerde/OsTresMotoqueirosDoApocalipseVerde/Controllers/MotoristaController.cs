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
        public async Task<IEnumerable<CreatedMotoristaResponse>> GetMotoristas()
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
        public async Task<ActionResult<CreatedMotoristaResponse>> GetMotorista(long id)
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
        /// Update motorista
        /// </summary>
        /// <param name="id"></param>
        /// <param name="motorista"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutMotorista(long id, Motorista motorista)
        {
            if (id != motorista.IdMotorista)
            {
                return BadRequest();
            }

            _repositoryMotorista.Update(motorista);

            return NoContent();
        }

        // POST: api/Motoristas
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Motorista>> PostMotorista(CreatedMotoristaRequest createdMotoristaRequest)
        {
            _validationMotorista.ValidateAndThrow(createdMotoristaRequest);
          
            var motoristaResponse = await _MotoristaUseCase.CreateMotorista(createdMotoristaRequest);
       
            return CreatedAtAction("GetMotorista", new { id = motoristaResponse.IdMotorista }, motoristaResponse);
        }
    }
}
