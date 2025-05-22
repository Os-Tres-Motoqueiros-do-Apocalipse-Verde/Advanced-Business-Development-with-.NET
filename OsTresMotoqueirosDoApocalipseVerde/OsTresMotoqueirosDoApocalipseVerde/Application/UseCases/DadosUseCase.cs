using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Persistence;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCases
{
    public class DadosUseCase
    {
        private readonly IRepository<Dados> _dadosRepository;

        public DadosUseCase(IRepository<Dados> dadosRepository)
        {
            _dadosRepository = dadosRepository;
        }

        public async Task<ReadDadosDto> CreateAsync(CreateDadosDto dto)
        {

            var dados = new Dados
            {
                CPF = dto.CPF,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Senha = dto.Senha,
                Nome = dto.Nome

            };

            await _dadosRepository.AddAsync(dados);

            return new ReadDadosDto
            {
                Id = dados.Id,
                CPF = dados.CPF,
                Telefone = dados.Telefone,
                Email = dados.Email,
                Senha = dados.Senha,
                Nome = dados.Nome
            };
        }

        public async Task<IEnumerable<ReadDadosDto>> GetAllAsync()
        {
            var dados = await _dadosRepository.GetAllAsync();
            return dados.Select(d => new ReadDadosDto
            {
                Id = d.Id,
                CPF = d.CPF,
                Telefone = d.Telefone,
                Email = d.Email,
                Senha = d.Senha,
                Nome = d.Nome
            });
        }

        public async Task<ReadDadosDto> GetByIdAsync(int id)
        {
            var dados = await _dadosRepository.GetByIdAsync(id);
            if (dados == null) return null;

            return new ReadDadosDto
            {
                Id = dados.Id,
                CPF = dados.CPF,
                Telefone = dados.Telefone,
                Email = dados.Email,
                Senha = dados.Senha,
                Nome = dados.Nome
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateDadosDto dto)
        {
            var dados = await _dadosRepository.GetByIdAsync(id);
            if (dados == null) return false;

            dados.CPF = dto.CPF;
            dados.Telefone = dto.Telefone;
            dados.Email = dto.Email;
            dados.Senha = dto.Senha;
            dados.Nome = dto.Nome;

            await _dadosRepository.UpdateAsync(dados);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dados = await _dadosRepository.GetByIdAsync(id);
            if (dados == null) return false;

            await _dadosRepository.DeleteAsync(dados);
            return true;
        }
    }
}