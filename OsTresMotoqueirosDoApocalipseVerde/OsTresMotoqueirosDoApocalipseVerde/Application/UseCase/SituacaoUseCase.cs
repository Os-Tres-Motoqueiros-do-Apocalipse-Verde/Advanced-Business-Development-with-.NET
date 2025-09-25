
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class SituacaoUseCase
    {
        private readonly IRepository<Situacao> _repository;

        public SituacaoUseCase(IRepository<Situacao> repository)
        {
            _repository = repository;
        }

        public async Task<CreateSituacaoResponse> CreateSituacaoAsync(CreateSituacaoRequest request)
        {
            var situacao = Situacao.Create(
                request.Nome,
                request.Descricao,
                request.Status
            );

            await _repository.AddAsync(situacao);
            await _repository.SaveChangesAsync();

            return new CreateSituacaoResponse
            {
                Id = situacao.Id,
                Nome = situacao.Nome,
                Descricao = situacao.Descricao,
                Status = situacao.Status
            };
        }

        public async Task<List<CreateSituacaoResponse>> GetAllSituacaoAsync()
        {
            var situacoes = await _repository.GetAllAsync();
            return situacoes.Select(u => new CreateSituacaoResponse
            {
                Id = u.Id,
                Nome = u.Nome,
                Descricao = u.Descricao,
                Status = u.Status
            }).ToList();
        }

        public async Task<CreateSituacaoResponse?> GetByIdAsync(long id)
        {
            var situacao = await _repository.GetByIdAsync(id);
            if (situacao == null) return null;

            return new CreateSituacaoResponse
            {
                Id = situacao.Id,
                Nome = situacao.Nome,
                Descricao = situacao.Descricao,
                Status = situacao.Status
            };
        }

        public async Task<bool> UpdateSituacaoAsync(long id, CreateSituacaoRequest request)
        {
            var situacao = await _repository.GetByIdAsync(id);
            if (situacao == null) return false;

            situacao.Atualizar(
                request.Nome,
                request.Descricao,
                request.Status
            );
            _repository.Update(situacao);
            await _repository.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteSituacaoAsync(long id)
        {
            var situacao = await _repository.GetByIdAsync(id);
            if (situacao == null) return false;

            _repository.Delete(situacao);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
