using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Persistence;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCases
{
    public class MotoristaUseCase
    {
        private readonly IRepository<Motorista> _repositoryMotorista;

        private readonly IRepository<Dados> _repositoryDados;

        public MotoristaUseCase(IRepository<Motorista> repositoryMotorista, IRepository<Dados> repositoryDados)
        {
            _repositoryMotorista = repositoryMotorista;
            _repositoryDados = repositoryDados;
        }

        public async Task<CreateMotoristaResponse> CreateMotorista(CreateMotoristaRequest createMotoristaRequest)
        {
            var dados = await _repositoryDados.GetByIdAsync(createMotoristaRequest.DadosId);

            var motorista = new Motorista(createMotoristaRequest.Plano, dados.Id);
            motorista.AtribuirDados(dados.CPF, dados.Telefone, dados.Email, dados.Senha, dados.Nome);


            await _repositoryMotorista.AddAsync(motorista);

            return new CreateMotoristaResponse { IdMotorista = motorista.IdMotorista, Plano = motorista.Plano, DadosId = motorista.DadosId };
        }

        public async Task<List<CreateMotoristaResponse>> GetAllMotoristaAsync()
        {
            var motoristas = await _repositoryMotorista.GetAllAsync();

            return motoristas.Select(b => new CreateMotoristaResponse
            {
                IdMotorista = b.IdMotorista,
                Plano = b.Plano,
                DadosId = b.DadosId
            }).ToList();
        }

        public async Task<CreateMotoristaResponse> GetByIdAsync(int IdMotorista)
        {
            var motorista = _repositoryMotorista.GetByIdAsync(IdMotorista).Result;

            return new CreateMotoristaResponse { IdMotorista = motorista.IdMotorista, Plano = motorista.Plano, DadosId = motorista.DadosId };
        }

        public async Task<bool> UpdateMotoristaAsync(int id, CreateMotoristaRequest updateRequest)
        {
            var motorista = await _repositoryMotorista.GetByIdAsync(id);
            if (motorista == null)
                return false;

            motorista.Plano = updateRequest.Plano;
            motorista.DadosId = updateRequest.DadosId;

            await _repositoryMotorista.UpdateAsync(motorista);

            return true;
        }



        public async Task<bool> DeleteMotoristaAsync(int id)
        {
            var motorista = await _repositoryMotorista.GetByIdAsync(id);

            if (motorista == null)
            {
                return false;
            }

            _repositoryMotorista.DeleteAsync(motorista);
            return true;
        }
    }
}
