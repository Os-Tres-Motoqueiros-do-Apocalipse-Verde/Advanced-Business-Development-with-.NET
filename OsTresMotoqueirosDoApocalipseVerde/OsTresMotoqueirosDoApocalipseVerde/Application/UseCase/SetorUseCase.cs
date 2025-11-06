using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class SetorUseCase
    {
        private readonly IRepository<Setor> _repository;
        private readonly AppDbContext _context;

        public SetorUseCase(IRepository<Setor> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<CreateSetorResponse> CreateSetorAsync(CreateSetorRequest request)
        {
            var setor = Setor.Create(
                request.NomeSetor,
                request.QtdMoto,
                request.Capacidade,
                request.Descricao,
                request.Cor,
                request.Localizacao,
                request.PatioId
            );

            await _repository.AddAsync(setor);
            await _repository.SaveChangesAsync();

            return new CreateSetorResponse
            {
                Id = setor.Id,
                NomeSetor = setor.NomeSetor,
                QtdMoto = setor.QtdMoto,
                Capacidade = setor.Capacidade,
                Descricao = setor.Descricao,
                Cor = setor.Cor,
                Localizacao = setor.Localizacao
            };
        }


        /// <summary>
        /// Retorna os Setor paginados.
        /// </summary>
        public async Task<List<CreateSetorResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var setor = await _repository.GetAllAsync();

            var paged = setor
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreateSetorResponse
                {
                    Id = u.Id,
                    NomeSetor = u.NomeSetor,
                    QtdMoto = u.QtdMoto,
                    Capacidade = u.Capacidade,
                    Descricao = u.Descricao,
                    Cor = u.Cor,
                    Localizacao = u.Localizacao
                })
                .ToList();

            return paged;
        }

        public async Task<CreateSetorResponse?> GetByIdAsync(long id)
        {
            var setor = await _context.Setor
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (setor == null) return null;

            return new CreateSetorResponse
            {
                Id = setor.Id,
                NomeSetor = setor.NomeSetor,
                QtdMoto = setor.QtdMoto,
                Capacidade = setor.Capacidade,
                Descricao = setor.Descricao,
                Cor = setor.Cor,
                Localizacao = setor.Localizacao,
                Patio = setor.Patio == null ? null : new CreatePatioResponse
                {
                    Id = setor.Patio.Id,
                    TotalMotos = setor.Patio.TotalMotos,
                    CapacidadeMoto = setor.Patio.CapacidadeMoto,
                    Localizacao = setor.Patio.Localizacao,
                    FilialId = setor.Patio.FilialId
                }
            };
        }

        public async Task<bool> UpdateSetorAsync(long id, CreateSetorRequest request)
        {
            var setor = await _repository.GetByIdAsync(id);
            if (setor == null) return false;

            setor.Atualizar(
                request.NomeSetor,
                request.QtdMoto,
                request.Capacidade,
                request.Descricao,
                request.Cor,
                request.Localizacao,
                request.PatioId
            );
            _repository.Update(setor);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSetorAsync(long id)
        {
            var setor = await _repository.GetByIdAsync(id);
            if (setor == null) return false;

            _repository.Delete(setor);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
