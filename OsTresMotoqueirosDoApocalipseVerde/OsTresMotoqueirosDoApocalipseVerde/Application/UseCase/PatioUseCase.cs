using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class PatioUseCase
    {
        private readonly IRepository<Patio> _repository;

        public PatioUseCase(IRepository<Patio> repository)
        {
            _repository = repository;
        }

        public async Task<CreatePatioResponse> CreatePatioAsync(CreatePatioRequest request)
        {
            var patio = Patio.Create(
                request.TotalMotos,
                request.CapacidadeMoto,
                request.Localizacao,
                request.FilialId
            );

            await _repository.AddAsync(patio);
            await _repository.SaveChangesAsync();

            return new CreatePatioResponse
            {
                Id = patio.Id,
                TotalMotos = patio.TotalMotos,
                CapacidadeMoto = patio.CapacidadeMoto,
                Localizacao = patio.Localizacao,
                FilialId = patio.FilialId
            };
        }

        public async Task<List<CreatePatioResponse>> GetAllPatioAsync()
        {
            var patio = await _repository.GetAllAsync();
            return patio.Select(u => new CreatePatioResponse
            {
                Id = u.Id,
                TotalMotos = u.TotalMotos,
                CapacidadeMoto = u.CapacidadeMoto,
                Localizacao = u.Localizacao,
                FilialId = u.FilialId
            }).ToList();
        }

        /// <summary>
        /// Retorna os Patio paginados.
        /// </summary>
        public async Task<List<CreatePatioResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var patio = await _repository.GetAllAsync();

            var paged = patio
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreatePatioResponse
                {
                    Id = u.Id,
                    TotalMotos = u.TotalMotos,
                    CapacidadeMoto = u.CapacidadeMoto,
                    Localizacao = u.Localizacao,
                    FilialId = u.FilialId
                })
                .ToList();

            return paged;
        }

        public async Task<CreatePatioResponse?> GetByIdAsync(long id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null) return null;

            return new CreatePatioResponse
            {
                Id = patio.Id,
                TotalMotos = patio.TotalMotos,
                CapacidadeMoto = patio.CapacidadeMoto,
                Localizacao = patio.Localizacao,
                FilialId = patio.FilialId
            };
        }

        public async Task<bool> UpdatePatioAsync(long id, CreatePatioRequest request)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null) return false;

            patio.Atualizar(
                request.TotalMotos,
                request.CapacidadeMoto,
                request.Localizacao,
                request.FilialId
            );
            _repository.Update(patio);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePatioAsync(long id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null) return false;

            _repository.Delete(patio);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
