
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
        private readonly AppDbContext _context;

        public PatioUseCase(
            IRepository<Patio> patioRepository,
            AppDbContext context)
        {
            _patioRepository = patioRepository;
            _context = context;
        }

        public async Task<CreatePatioResponse> CreatePatioAsync(CreatePatioRequest request)
        {

            var patio = Patio.Create(
                request.TotalMotos,
                request.CapacidadeMoto,
                request.Localizacao,
                request.FilialId);

            await _patioRepository.AddAsync(patio);
            await _patioRepository.SaveChangesAsync();

            return new CreatePatioResponse
            {

                TotalMotos = patio.TotalMotos,
                CapacidadeMoto = patio.CapacidadeMoto,
                Localizacao = patio.Localizacao,
                FilialId = patio.FilialId,

            };
        }

        public async Task<List<CreatePatioResponse>> GetAllPatioAsync()
        {
            var patios = await _patioRepository.GetAllAsync();
            var response = new List<CreatePatioResponse>();

            foreach (var patio in patios)
            {

                response.Add(new CreatePatioResponse
                {
                    Id = patio.Id,
                    TotalMotos = patio.TotalMotos,
                    CapacidadeMoto = patio.CapacidadeMoto,
                    Localizacao = patio.Localizacao,
                    FilialId = patio.FilialId,

                });
            }

            return response;
        }

        public async Task<CreatePatioResponse?> GetByIdAsync(long id)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            if (patio == null) return null;


            return new CreatePatioResponse
            {
                Id = patio.Id,
                TotalMotos = patio.TotalMotos,
                CapacidadeMoto = patio.CapacidadeMoto,
                Localizacao = patio.Localizacao,
                FilialId = patio.FilialId,
            };
        }

        public async Task<bool> UpdatePatioAsync(long id, CreatePatioRequest request)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            if (patio == null) return false;

            patio.Atualizar(
                request.TotalMotos,
                request.CapacidadeMoto,
                request.Localizacao,
                request.FilialId);


            _patioRepository.Update(patio);

            await _patioRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePatioAsync(long id)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            if (patio == null) return false;

            _patioRepository.Delete(patio);
            await _patioRepository.SaveChangesAsync();

            return true;
        }
    }
}
