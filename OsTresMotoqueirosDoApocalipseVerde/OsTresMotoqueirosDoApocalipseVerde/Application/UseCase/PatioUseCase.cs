using GB1.Infrastructure.Repositories;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class PatioUseCase
    {
        private readonly IRepository<Patio> _patioRepository;
        private readonly IRepository<Regiao> _regiaoRepository;
        private readonly AppDbContext _context;

        public PatioUseCase(
            IRepository<Patio> patioRepository,
            IRepository<Regiao> regiaoRepository,
            AppDbContext context)
        {
            _patioRepository = patioRepository;
            _regiaoRepository = regiaoRepository;
            _context = context;
        }

        public async Task<CreatePatioResponse> CreatePatioAsync(CreatePatioRequest request)
        {
            var regiao = Regiao.Create(
                request.Regiao.Localizacao
            );

            await _regiaoRepository.AddAsync(regiao);
            await _regiaoRepository.SaveChangesAsync();

            var patio = Patio.Create(
                request.TotalMotos,
                request.CapacidadeMoto,
                request.FilialId,
                regiao.Id);

            await _patioRepository.AddAsync(patio);
            await _patioRepository.SaveChangesAsync();

            return new CreatePatioResponse
            {

                TotalMotos = patio.TotalMotos,
                CapacidadeMoto = patio.CapacidadeMoto,
                FilialId = patio.FilialId,
                Regiao = new CreateRegiaoResponse
                {
                    Localizacao = regiao.Localizacao,
                    Area = regiao.Area,
                },

            };
        }

        public async Task<List<CreatePatioResponse>> GetAllPatioAsync()
        {
            var patios = await _patioRepository.GetAllAsync();
            var response = new List<CreatePatioResponse>();

            foreach (var patio in patios)
            {
                var regiao = await _regiaoRepository.GetByIdAsync(patio.RegiaoId.Value);

                response.Add(new CreatePatioResponse
                {
                    Id = patio.Id,
                    TotalMotos = patio.TotalMotos,
                    CapacidadeMoto = patio.CapacidadeMoto,
                    FilialId = patio.FilialId,
                    Regiao = new CreateRegiaoResponse
                    {
                        Localizacao = regiao.Localizacao,
                        Area = regiao.Area,
                    }

                });
            }

            return response;
        }

        public async Task<CreatePatioResponse?> GetByIdAsync(long id)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            if (patio == null) return null;

            var regiao = await _regiaoRepository.GetByIdAsync(patio.RegiaoId.Value);

            return new CreatePatioResponse
            {
                Id = patio.Id,
                TotalMotos = patio.TotalMotos,
                CapacidadeMoto = patio.CapacidadeMoto,
                FilialId = patio.FilialId,
                Regiao = new CreateRegiaoResponse
                {
                    Localizacao = regiao.Localizacao,
                    Area = regiao.Area,
                }
            };
        }

        public async Task<bool> UpdatePatioAsync(long id, CreatePatioRequest request)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            if (patio == null) return false;

            var regiao = await _regiaoRepository.GetByIdAsync(patio.RegiaoId.Value);
            if (regiao == null) return false;

            regiao.Atualizar(
                request.Regiao.Localizacao
            );
            patio.Atualizar(
                request.TotalMotos,
                request.CapacidadeMoto,
                request.FilialId,
                regiao.Id);


            _patioRepository.Update(patio);
            _regiaoRepository.Update(regiao);

            await _patioRepository.SaveChangesAsync();
            await _regiaoRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePatioAsync(long id)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            if (patio == null) return false;

            var regiaoId = patio.RegiaoId;

            _patioRepository.Delete(patio);
            await _patioRepository.SaveChangesAsync();

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
