using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class MotoristaUseCase
    {
        private readonly IRepository<Motorista> _repository;
        private readonly AppDbContext _context;

        public MotoristaUseCase(IRepository<Motorista> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<CreateMotoristaResponse> CreateMotoristaAsync(CreateMotoristaRequest request)
        {
            var motorista = Motorista.Create(
                request.Plano,
                request.DadosId
            );

            await _repository.AddAsync(motorista);
            await _repository.SaveChangesAsync();

            return new CreateMotoristaResponse
            {
                Id = motorista.Id,
                Plano = motorista.Plano
            };
        }

        /// <summary>
        /// Retorna os Motorista paginados.
        /// </summary>
        public async Task<List<CreateMotoristaResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var motorista = await _repository.GetAllAsync();

            var paged = motorista
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreateMotoristaResponse
                {
                    Id = u.Id,
                    Plano = u.Plano
                })
                .ToList();

            return paged;
        }

        public async Task<CreateMotoristaResponse?> GetByIdAsync(long id)
        {
            var motorista = await _context.Motorista
                .Include(m => m.Dados)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorista == null) return null;

            return new CreateMotoristaResponse
            {
                Id = motorista.Id,
                Plano = motorista.Plano,

                Dados = motorista.Dados == null ? null : new CreateDadosResponse
                {
                    Id = motorista.Dados.Id,
                    Nome = motorista.Dados.Nome,
                    CPF = motorista.Dados.CPF,
                    Telefone = motorista.Dados.Telefone,
                    Email = motorista.Dados.Email,
                    Senha = motorista.Dados.Senha
                }
            };
        }

        public async Task<bool> UpdateMotoristaAsync(long id, CreateMotoristaRequest request)
        {
            var motorista = await _repository.GetByIdAsync(id);
            if (motorista == null) return false;

            motorista.Atualizar(
                request.Plano,
                request.DadosId
            );
            _repository.Update(motorista);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMotoristaAsync(long id)
        {
            var motorista = await _repository.GetByIdAsync(id);
            if (motorista == null) return false;

            _repository.Delete(motorista);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
