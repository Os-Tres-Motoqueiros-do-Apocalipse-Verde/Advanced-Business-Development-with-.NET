using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Persistence;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCases
{
    public class DadosUseCase
    {
        private readonly IRepository<Dados> _repositoryDados;

        public DadosUseCase(IRepository<Dados> repositoryDados)
        {
            _repositoryDados = repositoryDados;
        }

        public async Task<CreateDadosResponse> CreateDados(CreateDadosRequest createDadosRequest)
        {
            var dados = new Dados(
                createDadosRequest.CPF,
                createDadosRequest.Telefone,
                createDadosRequest.Email,
                createDadosRequest.Senha,
                createDadosRequest.Nome
            );

            await _repositoryDados.AddAsync(dados);

            return new CreateDadosResponse
            {

                CPF = dados.CPF,
                Telefone = dados.Telefone,
                Email = dados.Email,
                Senha = dados.Senha,
                Nome = dados.Nome
            };
        }

        public async Task<List<CreateDadosResponse>> GetAllDadosAsync()
        {
            var dados = await _repositoryDados.GetAllAsync();

            return dados.Select(d => new CreateDadosResponse
            {
                Id = d.Id,
                CPF = d.CPF,
                Telefone = d.Telefone,
                Email = d.Email,
                Senha = d.Senha,
                Nome = d.Nome
            }).ToList();
        }

        public async Task<CreateDadosResponse> GetByIdAsync(int id)
        {
            var dados = await _repositoryDados.GetByIdAsync(id);

            if (dados == null)
                return null; 

            return new CreateDadosResponse
            {
                CPF = dados.CPF,
                Telefone = dados.Telefone,
                Email = dados.Email,
                Senha = dados.Senha,
                Nome = dados.Nome
            };
        }

        public async Task<bool> UpdateDadosAsync(int id, CreateDadosRequest updateRequest)
        {
            var dados = await _repositoryDados.GetByIdAsync(id);
            if (dados == null)
                return false;

            dados.CPF = updateRequest.CPF;
            dados.Telefone = updateRequest.Telefone;
            dados.Email = updateRequest.Email;
            dados.Senha = updateRequest.Senha;
            dados.Nome = updateRequest.Nome;

            await _repositoryDados.UpdateAsync(dados);

            return true;
        }



        public async Task<bool> DeleteDadosAsync(int id)
        {
            var dados = await _repositoryDados.GetByIdAsync(id);

            if (dados == null)
            {
                return false;
            }

            _repositoryDados.DeleteAsync(dados);
            return true;
        }
    }
}
