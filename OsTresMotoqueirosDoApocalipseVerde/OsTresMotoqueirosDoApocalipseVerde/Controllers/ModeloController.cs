using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure;

[ApiController]
[Route("api/[controller]")]
public class ModeloController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ModeloController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Create(CreateModeloDto dto)
    {
        var modelo = _mapper.Map<Modelo>(dto);
        _context.Modelo.Add(modelo);
        _context.SaveChanges();

        var readDto = _mapper.Map<ReadModeloDto>(modelo);
        return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, readDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReadModeloDto>> GetAll()
    {
        var modelos = _context.Modelo.ToList();
        return Ok(_mapper.Map<IEnumerable<ReadModeloDto>>(modelos));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var modelo = _context.Modelo.Find(id);
        if (modelo == null) return NotFound();

        return Ok(_mapper.Map<ReadModeloDto>(modelo));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var modelo = _context.Modelo.Find(id);
        if (modelo == null) return NotFound();

        _context.Modelo.Remove(modelo);
        _context.SaveChanges();
        return NoContent();
    }
}
