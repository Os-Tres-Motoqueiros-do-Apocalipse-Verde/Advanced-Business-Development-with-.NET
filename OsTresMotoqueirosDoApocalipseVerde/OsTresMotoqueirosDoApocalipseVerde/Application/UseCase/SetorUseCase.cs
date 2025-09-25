
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class SetorUseCase
    {
        private readonly IRepository<Setor> _setorRepository;
        private readonly AppDbContext _context;

        public SetorUseCase(
            IRepository<Setor> setorRepository,
            AppDbContext context)
        {
            _setorRepository = setorRepository;
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
                request.PatioId);

            await _setorRepository.AddAsync(setor);
            await _setorRepository.SaveChangesAsync();

            return new CreateSetorResponse
            {

                NomeSetor = setor.NomeSetor,
                QtdMoto = setor.QtdMoto,
                Capacidade = setor.Capacidade,
                Descricao = setor.Descricao,
                Cor = setor.Cor,
                Localizacao = setor.Localizacao,
                PatioId = setor.PatioId,

            };
        }

        public async Task<List<CreateSetorResponse>> GetAllSetorAsync()
        {
            var setores = await _setorRepository.GetAllAsync();
            var response = new List<CreateSetorResponse>();

            foreach (var setor in setores)
            {

                response.Add(new CreateSetorResponse
                {
                    Id = setor.Id,
                    NomeSetor = setor.NomeSetor,
                    QtdMoto = setor.QtdMoto,
                    Capacidade = setor.Capacidade,
                    Descricao = setor.Descricao,
                    Cor = setor.Cor,
                    Localizacao = setor.Localizacao,
                    PatioId = setor.PatioId,

                });
            }

            return response;
        }

        public async Task<CreateSetorResponse?> GetByIdAsync(long id)
        {
            var setor = await _setorRepository.GetByIdAsync(id);
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
                PatioId = setor.PatioId,

            };
        }

        public async Task<bool> UpdateSetorAsync(long id, CreateSetorRequest request)
        {
            var setor = await _setorRepository.GetByIdAsync(id);
            if (setor == null) return false;


            setor.Atualizar(
                request.NomeSetor,
                request.QtdMoto,
                request.Capacidade,
                request.Descricao,
                request.Cor,
                request.Localizacao,
                request.PatioId);


            _setorRepository.Update(setor);

            await _setorRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSetorAsync(long id)
        {
            var setor = await _setorRepository.GetByIdAsync(id);
            if (setor == null) return false;


            _setorRepository.Delete(setor);
            await _setorRepository.SaveChangesAsync();

            return true;
        }
    }
}
