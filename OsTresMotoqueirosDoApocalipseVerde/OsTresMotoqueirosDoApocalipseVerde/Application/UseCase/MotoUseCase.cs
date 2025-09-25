
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class MotoUseCase
    {
        private readonly IRepository<Moto> _motoRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly AppDbContext _context;

        public MotoUseCase(
            IRepository<Moto> motoRepository,
            IRepository<Modelo> modeloRepository,
            AppDbContext context)
        {
            _motoRepository = motoRepository;
            _modeloRepository = modeloRepository;
            _context = context;
        }

        public async Task<CreateMotoResponse> CreateMotoAsync(CreateMotoRequest request)
        {
            var modelo = Modelo.Create(
                request.Modelo.NomeModelo,
                request.Modelo.Frenagem,
                request.Modelo.SistemaPartida,
                request.Modelo.Tanque,
                request.Modelo.TipoCombustivel,
                request.Modelo.Consumo
            );

            await _modeloRepository.AddAsync(modelo);
            await _modeloRepository.SaveChangesAsync();

            var moto = Moto.Create(
                request.Placa, 
                request.Chassi, 
                request.Condicao, 
                request.LocalizacaoMoto, 
                request.MotoristaId, 
                request.SetorId, 
                request.SituacaoId,
                modelo.Id);

            await _motoRepository.AddAsync(moto);
            await _motoRepository.SaveChangesAsync();

            return new CreateMotoResponse
            {
                
                Placa = moto.Placa,
                Chassi = moto.Chassi,
                Condicao = moto.Condicao,
                LocalizacaoMoto = moto.LocalizacaoMoto,
                MotoristaId = moto.MotoristaId,
                SetorId = moto.SetorId,
                Modelo = new CreateModeloResponse
                {
                    NomeModelo = modelo.NomeModelo,
                    Frenagem = modelo.Frenagem,
                    SistemaPartida = modelo.SistemaPartida,
                    Tanque = modelo.Tanque,
                    TipoCombustivel = modelo.TipoCombustivel,
                    Consumo = modelo.Consumo,
                },

            };
        }

        public async Task<List<CreateMotoResponse>> GetAllMotoAsync()
        {
            var motos = await _motoRepository.GetAllAsync();
            var response = new List<CreateMotoResponse>();

            foreach (var moto in motos)
            {
                var modelo = await _modeloRepository.GetByIdAsync(moto.ModeloId);

                response.Add(new CreateMotoResponse
                {
                    Id = moto.Id,
                    Placa = moto.Placa,
                    Chassi = moto.Chassi,
                    Condicao = moto.Condicao,
                    LocalizacaoMoto = moto.LocalizacaoMoto,
                    MotoristaId = moto.MotoristaId,
                    SetorId = moto.SetorId,
                    Modelo = new CreateModeloResponse
                    {
                        NomeModelo = modelo.NomeModelo,
                        Frenagem = modelo.Frenagem,
                        SistemaPartida = modelo.SistemaPartida,
                        Tanque = modelo.Tanque,
                        TipoCombustivel = modelo.TipoCombustivel,
                        Consumo = modelo.Consumo,
                    }

                });
            }

            return response;
        }

        public async Task<CreateMotoResponse?> GetByIdAsync(long id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) return null;

            var modelo = await _modeloRepository.GetByIdAsync(moto.ModeloId);

            return new CreateMotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Chassi = moto.Chassi,
                Condicao = moto.Condicao,
                LocalizacaoMoto = moto.LocalizacaoMoto,
                MotoristaId = moto.MotoristaId,
                SetorId = moto.SetorId,
                Modelo = new CreateModeloResponse
                {
                    NomeModelo = modelo.NomeModelo,
                    Frenagem = modelo.Frenagem,
                    SistemaPartida = modelo.SistemaPartida,
                    Tanque = modelo.Tanque,
                    TipoCombustivel = modelo.TipoCombustivel,
                    Consumo = modelo.Consumo,
                }
            };
        }

        public async Task<bool> UpdateMotoAsync(long id, CreateMotoRequest request)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) return false;

            var modelo = await _modeloRepository.GetByIdAsync(moto.ModeloId);
            if (modelo == null) return false;

            modelo.Atualizar(
                request.Modelo.NomeModelo,
                request.Modelo.Frenagem,
                request.Modelo.SistemaPartida,
                request.Modelo.Tanque,
                request.Modelo.TipoCombustivel,
                request.Modelo.Consumo
            );
            moto.Atualizar(
                request.Placa, 
                request.Chassi, 
                request.Condicao, 
                request.LocalizacaoMoto, 
                request.MotoristaId, 
                request.SetorId,
                request.SituacaoId,
                modelo.Id);


            _motoRepository.Update(moto);
            _modeloRepository.Update(modelo);

            await _motoRepository.SaveChangesAsync();
            await _modeloRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMotoAsync(long id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) return false;

            var modeloId = moto.ModeloId;

            // Corrigido: aqui você deve deletar a moto
            _motoRepository.Delete(moto);
            await _motoRepository.SaveChangesAsync();

            var modelo = await _modeloRepository.GetByIdAsync(modeloId);
            if (modelo != null)
            {
                _modeloRepository.Delete(modelo);
                await _modeloRepository.SaveChangesAsync();
            }

            return true;
        }

    }
}
