using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class ModeloUseCase
    {
        private readonly IRepository<Filial> _repository;

        public FilialUseCase(IRepository<Filial> repository)
        {
            _repository = repository;
        }

        public async Task<CreateFilialResponse> CreateFilialAsync(CreateFilialRequest request)
        {
            var filial = Filial.Create(
                request.NomeFilial,
                request.EnderecoId
            );

            await _repository.AddAsync(filial);
            await _repository.SaveChangesAsync();

            return new CreateFilialResponse
            {
                Id = filial.Id,
                NomeFilial = filial.NomeFilial,
                EnderecoId = filial.EnderecoId
            };
        }

        public async Task<List<CreateFilialResponse>> GetAllFilialAsync()
        {
            var filiais = await _repository.GetAllAsync();
            return filiais.Select(u => new CreateFilialResponse
            {
                Id = u.Id,
                NomeFilial = u.NomeFilial,
                EnderecoId = u.EnderecoId
            }).ToList();
        }

        public async Task<CreateFilialResponse?> GetByIdAsync(long id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null) return null;

            return new CreateFilialResponse
            {
                Id = filial.Id,
                NomeFilial = filial.NomeFilial,
                EnderecoId = filial.EnderecoId
            };
        }

        public async Task<bool> UpdateFilialAsync(long id, CreateFilialRequest request)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null) return false;

            filial.Atualizar(
                request.NomeFilial,
                request.EnderecoId
            );
            _repository.Update(filial);
            await _repository.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteFilialAsync(long id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null) return false;

            _repository.Delete(filial);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
