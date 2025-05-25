using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("CRUD Modelo")]
    public class DadosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DadosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDadosDto dadosdto)
        {
            var dados = _mapper.Map<Dados>(dadosdto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dados.Add(dados);
            _context.SaveChanges();

            var readDadosDto = _mapper.Map<ReadDadosDto>(dados);
            return CreatedAtAction(nameof(RecuperarDadosPorId), new { Id = dados.Id }, readDadosDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadModeloDto>> RecuperarModelo()
        {
            var modelos = _context.Modelo.ToList();
            var modelosDto = _mapper.Map<List<ReadModeloDto>>(modelos);
            return Ok(modelosDto);
        }


        [HttpGet("{id}")]
        public IActionResult RecuperarDadosPorId(int id)
        {
            var dados = _context.Dados.FirstOrDefault(d => d.Id == id);
            if (dados != null)
            {
                var dadosDto = _mapper.Map<ReadDadosDto>(dados);
                return Ok(dadosDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarDados(int id, [FromBody] UpdateDadosDto dadosDto)
        {
            var dados = _context.Dados.FirstOrDefault(d => d.Id == id);
            if (dados == null)
            {
                return NotFound();
            }
            _mapper.Map(dadosDto, dados);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDados(int id)
        {
            var dados = _context.Dados.FirstOrDefault(d => d.Id == id);
            if (dados == null)
            {
                return NotFound();
            }
            _context.Remove(dados);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
