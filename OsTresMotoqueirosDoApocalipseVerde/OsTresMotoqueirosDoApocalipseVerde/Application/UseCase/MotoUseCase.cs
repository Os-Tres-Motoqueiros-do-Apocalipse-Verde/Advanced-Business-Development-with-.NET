using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class MotoUseCase
    {
        private readonly IRepository<Moto> _repository;
        private readonly AppDbContext _context;


        public MotoUseCase(IRepository<Moto> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
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
                LocalizacaoMoto = moto.LocalizacaoMoto
            };
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
            var moto = await _context.Moto
                .Include(m => m.Motorista)
                .Include(m => m.Modelo)
                .Include(m => m.Setor)
                .Include(m => m.Situacao)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null) return null;

            return new CreateMotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Chassi = moto.Chassi,
                Condicao = moto.Condicao,
                LocalizacaoMoto = moto.LocalizacaoMoto,

                Motorista = moto.Motorista == null ? null : new CreateMotoristaResponse
                {
                    Id = moto.Motorista.Id,
                    Plano = moto.Motorista.Plano
                },
                Modelo = moto.Modelo == null ? null : new CreateModeloResponse
                {
                    Id = moto.Modelo.Id,
                    NomeModelo = moto.Modelo.NomeModelo,
                    Frenagem = moto.Modelo.Frenagem,
                    SistemaPartida = moto.Modelo.SistemaPartida,
                    Tanque = moto.Modelo.Tanque,
                    TipoCombustivel = moto.Modelo.TipoCombustivel,
                    Consumo = moto.Modelo.Consumo
                },
                Setor = moto.Setor == null ? null : new CreateSetorResponse
                {
                    Id = moto.Setor.Id,
                    NomeSetor = moto.Setor.NomeSetor,
                    QtdMoto = moto.Setor.QtdMoto,
                    Capacidade = moto.Setor.Capacidade,
                    Descricao = moto.Setor.Descricao,
                    Cor = moto.Setor.Cor,
                    Localizacao = moto.Setor.Localizacao

                },
                situacao = moto.Situacao == null ? null : new CreateSituacaoResponse
                {
                    Id = moto.Situacao.Id,
                    Nome = moto.Situacao.Nome,
                    Descricao = moto.Situacao.Descricao,
                    Status = moto.Situacao.Status
                }
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
