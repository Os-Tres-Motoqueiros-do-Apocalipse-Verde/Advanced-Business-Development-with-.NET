using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure;

namespace OsTresMotoqueirosDoApocalipseVerde.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotoristaController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public MotoristaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarMotorista([FromBody] CreateMotoristaDto motoristaDto)
        {
            Motorista motorista = _mapper.Map<Motorista>(motoristaDto);

            _context.Motorista.Add(motorista);
            _context.SaveChanges();

            _context.Entry(motorista).Reference(m => m.Dados).Load();

            var readMotoristaDto = _mapper.Map<ReadMotoristaDto>(motorista);
            return CreatedAtAction(nameof(RecuperarMotoristaPorId), new { Id = motorista.Id }, readMotoristaDto);

        }

        [HttpGet]
        public IEnumerable<ReadMotoristaDto> RecuperarMotoristas()
        {
            var motoristas = _context.Motorista
                .Include(m => m.Dados) 
                .ToList();

            return _mapper.Map<List<ReadMotoristaDto>>(motoristas);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarMotoristaPorId(int id)
        {
            var motorista = _context.Motorista
                .Include(m => m.Dados) 
                .FirstOrDefault(m => m.Id == id);

            if (motorista == null)
            {
                return NotFound();
            }

            var motoristaDto = _mapper.Map<ReadMotoristaDto>(motorista);
            return Ok(motoristaDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarMotorista(int id, [FromBody] UpdateMotoristaDto motoristaDto)
        {
            Motorista motorista = _context.Motorista.FirstOrDefault(motoristas => motoristas.Id == id);
            if (motorista == null)
            {
                return NotFound();
            }
            _mapper.Map(motoristaDto, motorista);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMotorista(int id)
        {
            Motorista motorista = _context.Motorista.FirstOrDefault(motorista => motorista.Id == id);
            if (motorista == null)
            {
                return NotFound();
            }
            _context.Remove(motorista);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
