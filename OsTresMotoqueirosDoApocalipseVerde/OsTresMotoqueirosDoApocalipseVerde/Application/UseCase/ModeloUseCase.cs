using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class ModeloUseCase
    {
        private readonly IRepository<Modelo> _repository;

        public ModeloUseCase(IRepository<Modelo> repository)
        {
            _repository = repository;
        }

        public async Task<CreateModeloResponse> CreateModeloAsync(CreateModeloRequest request)
        {
            var modelo = Modelo.Create(
                request.NomeModelo,
                request.Frenagem,
                request.SistemaPartida,
                request.Tanque,
                request.TipoCombustivel,
                request.Consumo
            );

            await _repository.AddAsync(modelo);
            await _repository.SaveChangesAsync();

            return new CreateModeloResponse
            {
                Id = modelo.Id,
                NomeModelo = modelo.NomeModelo,
                Frenagem = modelo.Frenagem,
                SistemaPartida = modelo.SistemaPartida,
                Tanque = modelo.Tanque,
                TipoCombustivel = modelo.TipoCombustivel,
                Consumo = modelo.Consumo
            };
        }

        public async Task<List<CreateModeloResponse>> GetAllModeloAsync()
        {
            var modelo = await _repository.GetAllAsync();
            return modelo.Select(u => new CreateModeloResponse
            {
                Id = u.Id,
                NomeModelo = u.NomeModelo,
                Frenagem = u.Frenagem,
                SistemaPartida = u.SistemaPartida,
                Tanque = u.Tanque,
                TipoCombustivel = u.TipoCombustivel,
                Consumo = u.Consumo
            }).ToList();
        }

        /// <summary>
        /// Retorna os Modelo paginados.
        /// </summary>
        public async Task<List<CreateModeloResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var modelo = await _repository.GetAllAsync();

            var paged = modelo
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreateModeloResponse
                {
                    Id = u.Id,
                    NomeModelo = u.NomeModelo,
                    Frenagem = u.Frenagem,
                    SistemaPartida = u.SistemaPartida,
                    Tanque = u.Tanque,
                    TipoCombustivel = u.TipoCombustivel,
                    Consumo = u.Consumo
                })
                .ToList();

            return paged;
        }

        public async Task<CreateModeloResponse?> GetByIdAsync(long id)
        {
            var modelo = await _repository.GetByIdAsync(id);
            if (modelo == null) return null;

            return new CreateModeloResponse
            {
                Id = modelo.Id,
                NomeModelo = modelo.NomeModelo,
                Frenagem = modelo.Frenagem,
                SistemaPartida = modelo.SistemaPartida,
                Tanque = modelo.Tanque,
                TipoCombustivel = modelo.TipoCombustivel,
                Consumo = modelo.Consumo
            };
        }

        public async Task<bool> UpdateModeloAsync(long id, CreateModeloRequest request)
        {
            var modelo = await _repository.GetByIdAsync(id);
            if (modelo == null) return false;

            modelo.Atualizar(
                request.NomeModelo,
                request.Frenagem,
                request.SistemaPartida,
                request.Tanque,
                request.TipoCombustivel,
                request.Consumo
            );
            _repository.Update(modelo);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteModeloAsync(long id)
        {
            var modelo = await _repository.GetByIdAsync(id);
            if (modelo == null) return false;

            _repository.Delete(modelo);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
