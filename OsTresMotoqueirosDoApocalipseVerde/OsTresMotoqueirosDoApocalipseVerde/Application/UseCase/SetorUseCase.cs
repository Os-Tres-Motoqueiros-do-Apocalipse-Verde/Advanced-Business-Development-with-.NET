using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class SetorUseCase
    {
        private readonly IRepository<Setor> _repository;

        public SetorUseCase(IRepository<Setor> repository)
        {
            _repository = repository;
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
                Localizacao = setor.Localizacao,
                PatioId = setor.PatioId
            };
        }

        public async Task<List<CreateSetorResponse>> GetAllSetorAsync()
        {
            var setores = await _repository.GetAllAsync();
            return setores.Select(u => new CreateSetorResponse
            {
                Id = u.Id,
                NomeSetor = u.NomeSetor,
                QtdMoto = u.QtdMoto,
                Capacidade = u.Capacidade,
                Descricao = u.Descricao,
                Cor = u.Cor,
                Localizacao=u.Localizacao,
                PatioId = u.PatioId
            }).ToList();
        }

        public async Task<CreateSetorResponse?> GetByIdAsync(long id)
        {
            var setor = await _repository.GetByIdAsync(id);
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
                PatioId = setor.PatioId
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
