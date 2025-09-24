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
    [Tags("CRUD Moto")]
    public class MotoController : ControllerBase
    {
        private readonly MotoUseCase _motoUseCase;
        private readonly CreateMotoDtoValidator _validationMoto;

        public MotoController(
            MotoUseCase motoUseCase,
            CreateMotoDtoValidator validationMoto)
        {
            _motoUseCase = motoUseCase;
            _validationMoto = validationMoto;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateMotoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMotos()
        {
            var filiais = await _motoUseCase.GetAllMotoAsync();
            return Ok(filiais);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateMotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMoto(long id)
        {
            var moto = await _motoUseCase.GetByIdAsync(id);
            if (moto == null)
                return NotFound();

            return Ok(moto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateMotoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostMoto([FromBody] CreateMotoRequest request)
        {
            var result = ((IValidator<CreateMotoRequest>)_validationMoto).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var motoResponse = await _motoUseCase.CreateMotoAsync(request);
            return CreatedAtAction(nameof(GetMoto), new { id = motoResponse.Id }, motoResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutMoto(long id, [FromBody] CreateMotoRequest request)
        {
            var result = ((IValidator<CreateMotoRequest>)_validationMoto).Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updated = await _motoUseCase.UpdateMotoAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

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
