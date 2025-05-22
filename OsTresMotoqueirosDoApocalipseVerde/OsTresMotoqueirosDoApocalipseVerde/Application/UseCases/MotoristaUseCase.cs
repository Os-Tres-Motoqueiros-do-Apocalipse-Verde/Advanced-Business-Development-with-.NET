using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Persistence;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCases
{
    public class MotoristaUseCase
    {
        private readonly IRepository<Motorista> _motoristaRepository;
        private readonly IRepository<Dados> _dadosRepository;

        public MotoristaUseCase(IRepository<Motorista> motoristaRepository, IRepository<Dados> dadosRepository)
        {
            _motoristaRepository = motoristaRepository;
            _dadosRepository = dadosRepository;
        }

        public async Task<ReadMotoristaDto> CreateAsync(CreateMotoristaDto dto)
        {
            Dados dados = null;

            if (dto.Dados != null)
            {
                dados = new Dados
                {
                    CPF = dto.Dados.CPF,
                    Telefone = dto.Dados.Telefone,
                    Email = dto.Dados.Email,
                    Senha = dto.Dados.Senha,
                    Nome = dto.Dados.Nome
                };

                await _dadosRepository.AddAsync(dados);
            }

            var motorista = new Motorista
            {
                Plano = (Plano)dto.Plano,
                Dados = dados,
                DadosId = dados?.Id
            };

            await _motoristaRepository.AddAsync(motorista);

            return new ReadMotoristaDto
            {
                IdMotorista = motorista.Id,
                Plano = motorista.Plano,
                DadosId = motorista.DadosId
            };
        }

        public async Task<IEnumerable<ReadMotoristaDto>> GetAllAsync()
        {
            var motoristas = await _motoristaRepository.GetAllAsync();
            return motoristas.Select(m => new ReadMotoristaDto
            {
                IdMotorista = m.Id,
                Plano = m.Plano,
                DadosId = m.DadosId
            });
        }

        public async Task<ReadMotoristaDto> GetByIdAsync(int id)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id);
            if (motorista == null) return null;

            return new ReadMotoristaDto
            {
                IdMotorista = motorista.Id,
                Plano = motorista.Plano,
                DadosId = motorista.DadosId
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateMotoristaDto dto)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id);
            if (motorista == null) return false;

            motorista.Plano = (Plano)dto.Plano;
            motorista.DadosId = dto.DadosId;

            await _motoristaRepository.UpdateAsync(motorista);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id);
            if (motorista == null) return false;

            await _motoristaRepository.DeleteAsync(motorista);
            return true;
        }
    }
}