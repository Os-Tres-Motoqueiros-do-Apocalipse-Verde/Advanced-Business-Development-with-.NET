using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class MotoUseCase
    {
        private readonly IRepository<Moto> _repository;

        public MotoUseCase(IRepository<Moto> repository)
        {
            _repository = repository;
        }

        public async Task<CreateMotoResponse> CreateMotoAsync(CreateMotoRequest request)
        {
            var moto = Moto.Create(
                request.Placa,
                request.Chassi,
                request.Condicao,
                request.LocalizacaoMoto,
                request.MotoristaId,
                request.ModeloId,
                request.SetorId,
                request.SituacaoId
            );

            await _repository.AddAsync(moto);
            await _repository.SaveChangesAsync();

            return new CreateMotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Chassi = moto.Chassi,
                Condicao = moto.Condicao,
                LocalizacaoMoto = moto.LocalizacaoMoto,
                MotoristaId = moto.MotoristaId,
                ModeloId = moto.ModeloId,
                SetorId = moto.SetorId,
                SituacaoId = moto.SituacaoId
            };
        }

        public async Task<List<CreateMotoResponse>> GetAllMotoAsync()
        {
            var moto = await _repository.GetAllAsync();
            return moto.Select(u => new CreateMotoResponse
            {
                Id = u.Id,
                Placa = u.Placa,
                Chassi = u.Chassi,
                Condicao = u.Condicao,
                LocalizacaoMoto = u.LocalizacaoMoto,
                MotoristaId = u.MotoristaId,
                ModeloId = u.ModeloId,
                SetorId = u.SetorId,
                SituacaoId = u.SituacaoId
            }).ToList();
        }

        /// <summary>
        /// Retorna os Moto paginados.
        /// </summary>
        public async Task<List<CreateMotoResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var moto = await _repository.GetAllAsync();

            var paged = moto
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreateMotoResponse
                {
                    Id = u.Id,
                    Placa = u.Placa,
                    Chassi = u.Chassi
                })
                .ToList();

            return paged;
        }

        public async Task<CreateMotoResponse?> GetByIdAsync(long id)
        {
            var moto = await _repository.GetByIdAsync(id);
            if (moto == null) return null;

            return new CreateMotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Chassi = moto.Chassi,
                Condicao = moto.Condicao,
                LocalizacaoMoto = moto.LocalizacaoMoto,
                MotoristaId = moto.MotoristaId,
                ModeloId = moto.ModeloId,
                SetorId = moto.SetorId,
                SituacaoId = moto.SituacaoId
            };
        }

        public async Task<bool> UpdateMotoAsync(long id, CreateMotoRequest request)
        {
            var moto = await _repository.GetByIdAsync(id);
            if (moto == null) return false;

            moto.Atualizar(
                request.Placa,
                request.Chassi,
                request.Condicao,
                request.LocalizacaoMoto,
                request.MotoristaId,
                request.ModeloId,
                request.SetorId,
                request.SituacaoId
            );
            _repository.Update(moto);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMotoAsync(long id)
        {
            var moto = await _repository.GetByIdAsync(id);
            if (moto == null) return false;

            _repository.Delete(moto);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
