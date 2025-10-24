using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class MotoMLController : ControllerBase
    {
        private readonly MotoPredictionUseCase _motoPredictionUseCase;

        public MotoMLController(MotoPredictionUseCase motoPredictionUseCase)
        {
            _motoPredictionUseCase = motoPredictionUseCase;
        }

        /// <summary>
        /// Treina o modelo de Machine Learning com base nas motos cadastradas no banco de dados.
        /// </summary>
        [HttpPost("treinar")]
        public async Task<IActionResult> TreinarModelo()
        {
            try
            {
                var resultado = await _motoPredictionUseCase.TreinarModeloAsync();
                return Ok(new { mensagem = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        /// <summary>
        /// Faz uma previsão da condição da moto com base em ModeloId, SetorId e SituacaoId.
        /// </summary>
        [HttpPost("prever")]
        public IActionResult PreverCondicao([FromBody] MotoPrevisaoRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { erro = "Os dados de entrada são obrigatórios." });

                var resultado = _motoPredictionUseCase.PreverCondicao(request.ModeloId, request.SetorId, request.SituacaoId);
                return Ok(new { mensagem = resultado });
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }

    /// <summary>
    /// DTO de entrada para o endpoint de previsão.
    /// </summary>
    public class MotoPrevisaoRequest
    {
        /// <example>1</example>
        public float ModeloId { get; set; }

        /// <example>2</example>
        public float SetorId { get; set; }

        /// <example>3</example>
        public float SituacaoId { get; set; }
    }
}
