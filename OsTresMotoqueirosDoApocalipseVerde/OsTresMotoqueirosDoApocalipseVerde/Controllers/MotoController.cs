using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;

[ApiController]
[Route("api/[controller]")]
[Tags("CRUD Moto")]
public class MotoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MotoController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Create(CreateMotoDto dto)
    {
        var moto = _mapper.Map<Moto>(dto);
        _context.Moto.Add(moto);
        _context.SaveChanges();

        var readDto = _mapper.Map<ReadMotoDto>(
            _context.Moto.Include(m => m.Modelo).First(m => m.Id == moto.Id)
        );
        return CreatedAtAction(nameof(GetById), new { id = moto.Id }, readDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReadMotoDto>> GetAll()
    {
        var motos = _context.Moto.Include(m => m.Modelo).ToList();
        return Ok(_mapper.Map<IEnumerable<ReadMotoDto>>(motos));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var moto = _context.Moto.Include(m => m.Modelo).FirstOrDefault(m => m.Id == id);
        if (moto == null) return NotFound();

        return Ok(_mapper.Map<ReadMotoDto>(moto));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var moto = _context.Moto.Find(id);
        if (moto == null) return NotFound();

        _context.Moto.Remove(moto);
        _context.SaveChanges();
        return NoContent();
    }
}
