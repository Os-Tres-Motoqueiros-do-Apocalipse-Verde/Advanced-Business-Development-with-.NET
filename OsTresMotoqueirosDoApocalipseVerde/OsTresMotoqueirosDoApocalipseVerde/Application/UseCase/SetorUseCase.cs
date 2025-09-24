using GB1.Infrastructure.Repositories;
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
        private readonly IRepository<Regiao> _regiaoRepository;
        private readonly AppDbContext _context;

        public SetorUseCase(
            IRepository<Setor> setorRepository,
            IRepository<Regiao> regiaoRepository,
            AppDbContext context)
        {
            _setorRepository = setorRepository;
            _regiaoRepository = regiaoRepository;
            _context = context;
        }

        public async Task<CreateSetorResponse> CreateSetorAsync(CreateSetorRequest request)
        {
            var regiao = Regiao.Create(
                request.Regiao.Localizacao
            );

            await _regiaoRepository.AddAsync(regiao);
            await _regiaoRepository.SaveChangesAsync();

            var setor = Setor.Create(
                request.NomeSetor,
                request.QtdMoto,
                request.Capacidade,
                request.Descricao,
                request.Cor,
                request.PatioId,
                regiao.Id);

            await _setorRepository.AddAsync(setor);
            await _setorRepository.SaveChangesAsync();

            return new CreateSetorResponse
            {

                NomeSetor = setor.NomeSetor,
                QtdMoto = setor.QtdMoto,
                Capacidade = setor.Capacidade,
                Descricao = setor.Descricao,
                Cor = setor.Cor,
                PatioId = setor.PatioId,
                Regiao = new CreateRegiaoResponse
                {
                    Localizacao = regiao.Localizacao,
                    Area = regiao.Area,
                },

            };
        }

        public async Task<List<CreateSetorResponse>> GetAllSetorAsync()
        {
            var setores = await _setorRepository.GetAllAsync();
            var response = new List<CreateSetorResponse>();

            foreach (var setor in setores)
            {
                var regiao = await _regiaoRepository.GetByIdAsync(setor.RegiaoId.Value);

                response.Add(new CreateSetorResponse
                {
                    Id = setor.Id,
                    NomeSetor = setor.NomeSetor,
                    QtdMoto = setor.QtdMoto,
                    Capacidade = setor.Capacidade,
                    Descricao = setor.Descricao,
                    Cor = setor.Cor,
                    PatioId = setor.PatioId,
                    Regiao = new CreateRegiaoResponse
                    {
                        Localizacao = regiao.Localizacao,
                        Area = regiao.Area,
                    }

                });
            }

            return response;
        }

        public async Task<CreateSetorResponse?> GetByIdAsync(long id)
        {
            var setor = await _setorRepository.GetByIdAsync(id);
            if (setor == null) return null;

            var regiao = await _regiaoRepository.GetByIdAsync(setor.RegiaoId.Value);

            return new CreateSetorResponse
            {
                Id = setor.Id,
                NomeSetor = setor.NomeSetor,
                QtdMoto = setor.QtdMoto,
                Capacidade = setor.Capacidade,
                Descricao = setor.Descricao,
                Cor = setor.Cor,
                PatioId = setor.PatioId,
                Regiao = new CreateRegiaoResponse
                {
                    Localizacao = regiao.Localizacao,
                    Area = regiao.Area,
                }
            };
        }

        public async Task<bool> UpdateSetorAsync(long id, CreateSetorRequest request)
        {
            var setor = await _setorRepository.GetByIdAsync(id);
            if (setor == null) return false;

            var regiao = await _regiaoRepository.GetByIdAsync(setor.RegiaoId.Value);
            if (regiao == null) return false;

            regiao.Atualizar(
                request.Regiao.Localizacao
            );
            setor.Atualizar(
                request.NomeSetor,
                request.QtdMoto,
                request.Capacidade,
                request.Descricao,
                request.Cor,
                request.PatioId,
                regiao.Id);


            _setorRepository.Update(setor);
            _regiaoRepository.Update(regiao);

            await _setorRepository.SaveChangesAsync();
            await _regiaoRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSetorAsync(long id)
        {
            var setor = await _setorRepository.GetByIdAsync(id);
            if (setor == null) return false;

            var regiaoId = setor.RegiaoId;

            _setorRepository.Delete(setor);
            await _setorRepository.SaveChangesAsync();

            if (regiaoId.HasValue)
            {
                var regiao = await _regiaoRepository.GetByIdAsync(regiaoId.Value);
                if (regiao != null)
                {
                    _regiaoRepository.Delete(regiao);
                    await _regiaoRepository.SaveChangesAsync();
                }
            }

            return true;
        }
    }
}
