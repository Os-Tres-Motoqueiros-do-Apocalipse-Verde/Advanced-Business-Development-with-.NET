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

        public async Task<CreatedMotoristaResponse> CreateMotorista(CreatedMotoristaRequest createMotoristaRequest)
        {
            var dados = await _repositoryDados.GetByIdAsync(createMotoristaRequest.DadosId) /*?? throw new Exception("Brand invalid")*/;

            var motorista = new Motorista(createMotoristaRequest.Plano, dados.Id);
            motorista.AtribuirDados(dados.CPF, dados.Telefone, dados.Email, dados.Senha, dados.Nome);


            await _repositoryMotorista.AddAsync(motorista);

            return new CreatedMotoristaResponse { IdMotorista = motorista.IdMotorista, Plano = motorista.Plano };
        }

        public async Task<List<CreatedMotoristaResponse>> GetAllMotoristaAsync()
        {
            var motoristas = _repositoryMotorista.GetAllAsync().Result.ToList();

            return motoristas.Select(b => new CreatedMotoristaResponse
            {
                IdMotorista = b.IdMotorista,
                Plano = b.Plano,
            }).ToList();
        }

        public async Task<CreatedMotoristaResponse> GetByIdAsync(long IdMotorista)
        {
            var motorista = _repositoryMotorista.GetByIdAsync(IdMotorista).Result;

            return new CreatedMotoristaResponse { Plano = motorista.Plano };
        }

        public void UpdateMotorista(long IdMotorista, Motorista motorista)
        {
            
        }
    }
}
