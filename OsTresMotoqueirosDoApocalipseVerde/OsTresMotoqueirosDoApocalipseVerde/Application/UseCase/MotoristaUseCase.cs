using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class MotoristaUseCase
    {
        private readonly IRepository<Motorista> _repository;

        public MotoristaUseCase(IRepository<Motorista> repository)
        {
            _repository = repository;
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
                Plano = motorista.Plano,
                DadosId = motorista.DadosId
            };
        }

        public async Task<List<CreateMotoristaResponse>> GetAllMotoristaAsync()
        {
            var motorista = await _repository.GetAllAsync();
            return motorista.Select(u => new CreateMotoristaResponse
            {
                Id = u.Id,
                Plano = u.Plano,
                DadosId = u.DadosId
            }).ToList();
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
                    Plano = u.Plano,
                    DadosId = u.DadosId
                })
                .ToList();

            return paged;
        }

        public async Task<CreateMotoristaResponse?> GetByIdAsync(long id)
        {
            var motorista = await _repository.GetByIdAsync(id);
            if (motorista == null) return null;

            return new CreateMotoristaResponse
            {
                Id = motorista.Id,
                Plano = motorista.Plano,
                DadosId = motorista.DadosId
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
