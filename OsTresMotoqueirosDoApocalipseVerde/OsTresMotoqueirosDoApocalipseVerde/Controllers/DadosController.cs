using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DadosController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public DadosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarDados([FromBody] CreateDadosDto dadosDto)
        {
            Dados dados = _mapper.Map<Dados>(dadosDto);
            _context.Dados.Add(dados);
            _context.SaveChanges();

            var readDadosDto = _mapper.Map<ReadDadosDto>(dados);
            return CreatedAtAction(nameof(RecuperarDadosPorId), new { Id = dados.Id }, readDadosDto);

        }

        [HttpGet]
        public IEnumerable<ReadDadosDto> RecuperarDados()
        {
            return _mapper.Map<List<ReadDadosDto>>(_context.Dados.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarDadosPorId(int id)
        {
            Dados dados = _context.Dados.FirstOrDefault(dados => dados.Id == id);
            if (dados != null)
            {
                ReadDadosDto dadosDto = _mapper.Map<ReadDadosDto>(dados);
                return Ok(dadosDto);
            }
            return NotFound();
        }


        [HttpPut("{id}")]
        public IActionResult AtualizarDados(int id, [FromBody] UpdateDadosDto dadosDto)
        {
            Dados dados = _context.Dados.FirstOrDefault(dados => dados.Id == id);
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
            Dados dados = _context.Dados.FirstOrDefault(dados => dados.Id == id);
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
